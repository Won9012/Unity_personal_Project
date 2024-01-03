using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeStamp : MonoBehaviour
{
    public int year;

    private void Update()
    {
        print(second);
    }

    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }

    public Season season;
    public int day;
    public int hour;
    public int minute;
    public float second;

    public GameTimeStamp(int year, Season season, int day, int hour, int minute, float second)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    public void UpdateTime()
    {
        second += Time.deltaTime;

    }
}
