using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    [SerializeField] private GameTimeStamp timeStamp;

    public float timeScale = 1.0f;

    //빛 조절
    public Transform sunTransform;
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
            yield return new WaitForSeconds(1 / timeScale);
            Tick();
        }
    }

    public void Tick()
    {
        timeStamp.UpdateTime();

        int timeInMinutes = GameTimeStamp.HoursToMinutes(timeStamp.hour) + timeStamp.minute;

        //태양은 1시간에 15도 움직임 (24시간 360도)
        //0.25f 각도는 1분당 움직이는 각도
        //0:00분에 태양각은 -90
        float sunAngle = .25f * timeInMinutes - 90;

        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }

}
