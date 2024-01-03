using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeTraker 
{
    void ClockUpdate(GameTimeStamp timeStamp);
}
