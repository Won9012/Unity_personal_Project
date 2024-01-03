using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("Internal Clock")]
    [SerializeField] private GameTimeStamp timeStamp;

    public float timeScale = 1.0f;

    [Header("Day & Night Cycle")]
    //�� ����
    public Transform sunTransform;

    List<ITimeTraker> listeners = new List<ITimeTraker>();
    private void Awake()
    {
        if(Instance !=null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        timeStamp = new GameTimeStamp(0, GameTimeStamp.Season.Spring, 1, 6, 0, 0);
        StartCoroutine(TimeUpdate());
    }


    IEnumerator TimeUpdate()
    {
        while (true)
        {
            Tick();
            yield return new WaitForSeconds(1 / timeScale);
        }
    }

    public void Tick()
    {
        timeStamp.UpdateTime();

        //inform the listeners of the new time state
        foreach (ITimeTraker listener in listeners)
        {
            listener.ClockUpdate(timeStamp);
        }
        UpdateSunMovement();
    }

    public GameTimeStamp GetGameTimestamp()
    {
        //�����Ѿ� ��������
        return new GameTimeStamp(timeStamp);
    }


    //�㳷 
    void UpdateSunMovement()
    {

        int timeInMinutes = GameTimeStamp.HoursToMinutes(timeStamp.hour) + timeStamp.minute;

        //�¾��� 1�ð��� 15�� ������ (24�ð� 360��)
        //0.25f ������ 1�д� �����̴� ����
        //0:00�п� �¾簢�� -90
        float sunAngle = .25f * timeInMinutes - 90;

        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }


    //�ڵ鸵

    //������ ������Ʈ �߰�
    public void ResgisterTracker(ITimeTraker listener)
    {
        listeners.Add(listener);
    }
    //����
    public void UnregisterTracker(ITimeTraker listener)
    {
        listeners.Remove(listener);
    }
}
