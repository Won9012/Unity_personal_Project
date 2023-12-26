using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RideCar : MonoBehaviour
{
    [SerializeField] private GameObject middle;
    [SerializeField] private GameObject Ride_btn;


    //차를 타지 않은 상황에서 차근처에 접근을 했을때 상황
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Ride_btn.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Ride_btn.SetActive(false);
        
    }
}
