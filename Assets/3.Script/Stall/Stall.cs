using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stall : MonoBehaviour
{
    public GameObject StallUI;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StallUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StallUI.SetActive(false);
    }




}

    
