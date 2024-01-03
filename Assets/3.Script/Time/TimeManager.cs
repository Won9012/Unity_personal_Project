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
    //빛 조절
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
        //복사한애 가져오기
        return new GameTimeStamp(timeStamp);
    }


    //밤낮 
    void UpdateSunMovement()
    {

        int timeInMinutes = GameTimeStamp.HoursToMinutes(timeStamp.hour) + timeStamp.minute;

        //태양은 1시간에 15도 움직임 (24시간 360도)
        //0.25f 각도는 1분당 움직이는 각도
        //0:00분에 태양각은 -90
        float sunAngle = .25f * timeInMinutes - 90;

        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }


    //핸들링

    //리스너 오브젝트 추가
    public void ResgisterTracker(ITimeTraker listener)
    {
        listeners.Add(listener);
    }
    //제거
    public void UnregisterTracker(ITimeTraker listener)
    {
        listeners.Remove(listener);
    }
}
