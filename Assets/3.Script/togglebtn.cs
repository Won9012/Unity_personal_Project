using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class togglebtn : MonoBehaviour
{
    [SerializeField] GameObject gameobj;

    public void ToggleUi()
    {
        if (gameobj.activeSelf)
        {
            gameobj.SetActive(false);
        }
        else
        {
            gameobj.SetActive(true);
        }
    }
}
