using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTimeStamp
{
    public int year;

    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }

    public Season season;

    public enum DayOfTheWeek
    {
        토요일,
        일요일,
        월요일,
        화요일,
        수요일,
        목요일,
        금요일
    }
    public int day;
    public int hour;
    public int minute;
    public int second;

    public GameTimeStamp(int year, Season season, int day, int hour, int minute, int second)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
        this.second = second;
    }
    //복제
    public GameTimeStamp(GameTimeStamp timeStamp)
    {
        this.year = timeStamp.year;
        this.season = timeStamp.season;
        this.day = timeStamp.day;
        this.hour = timeStamp.hour;
        this.minute = timeStamp.minute;
        this.second = timeStamp.second;
    }

    public void UpdateTime()
    {
        //second++;
        minute++;
        if(second >= 60)
        {
            second = 0;
            minute++;
        }

        if(minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if(hour >= 24)
        {
            hour = 0;
            day++;
        }

        //한달을 기준으로 계절을 바꿀것.
        if(day > 30)
        {
            day = 1;

            if(season == Season.Winter)
            {
                season = Season.Spring;
                year++;
            }
            else
            {

                season++;
            }
        
        }
    }

    public DayOfTheWeek GetDayofTheWeek()
    {
        int daysPassed = YearsToDays(year) + SeasonsToDays(season)+day;

        int dayIndex = daysPassed % 7;
        return (DayOfTheWeek)dayIndex;
    }

    public static int HoursToMinutes(int hour)
    {
        return hour * 60;
    }
    public static int HoursToSecond(int minuts)
    {
        return minuts * 60;
    }



    public static int DaysToHours(int days)
    {
        return days * 24;
    }



    public static int SeasonsToDays(Season season)
    {
        int seasonIndex = (int)season;
        return seasonIndex * 30;
    }

    public static int YearsToDays(int years)
    {
        return years * 4 * 30;
    }


    //2가지의 시간을 비교해서, 24시간을 판정?
    public static int CompareTimestamps(GameTimeStamp timestamp1, GameTimeStamp timestamp2)
    {
        int timestap1Hours = DaysToHours(YearsToDays(timestamp1.year)) + DaysToHours(SeasonsToDays(timestamp1.season)) + DaysToHours(timestamp1.day) + timestamp1.hour;
        int timestap2Hours = DaysToHours(YearsToDays(timestamp2.year)) + DaysToHours(SeasonsToDays(timestamp2.season)) + DaysToHours(timestamp2.day) + timestamp2.hour;
        int diffrence = timestap2Hours - timestap1Hours;
        return Mathf.Abs(diffrence);
    }
}
