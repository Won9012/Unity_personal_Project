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
        //TimeManager�� �ð� ������Ʈ �� �˸� uiManager�� ��ü ��Ͽ� �߰�.
        TimeManager.Instance.ResgisterTracker(this);
    }
    //UI�� ��������..
    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        //�ð�����

        //�ð� , ��
        int hours = timeStamp.hour;
        int minutes = timeStamp.minute;
        float second = timeStamp.second;

        //Am or PM
        string prefix = "AM ";

        //AM->PM �ٲ�ġ��
        if (hours > 12)
        {
            prefix = "PM ";
            hours -= 12;
        }

        timeText.text = $"{prefix} : {hours}�� {minutes.ToString("00")}�� {second.ToString("00")}��";

        //��¥����
        int day = timeStamp.day;

        string season = timeStamp.season.ToString();
        string dayOfTheWeek = timeStamp.GetDayofTheWeek().ToString();

        //format it for the date text display
        dateText.text = $"{season} {day}�� ({dayOfTheWeek})";
    }






}
