using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour, ITimeTraker
{
    public static UImanager Instance { get; private set; }

    public Text timeText;
    public Text dateText;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        //TimeManager가 시간 업데이트 시 알림 uiManager를 개체 목록에 추가.
        TimeManager.Instance.ResgisterTracker(this);
    }
    //UI에 연동하자..
    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        //시간관리

        //시간 , 분
        int hours = timeStamp.hour;
        int minutes = timeStamp.minute;
        float second = timeStamp.second;

        //Am or PM
        string prefix = "AM ";

        //AM->PM 바꿔치기
        if (hours > 12)
        {
            prefix = "PM ";
            hours -= 12;
        }

        timeText.text = $"{prefix} : {hours}시 {minutes.ToString("00")}분 {second.ToString("00")}초";

        //날짜번경
        int day = timeStamp.day;

        string season = timeStamp.season.ToString();
        string dayOfTheWeek = timeStamp.GetDayofTheWeek().ToString();

        //format it for the date text display
        dateText.text = $"{season} {day}일 ({dayOfTheWeek})";
    }






}
