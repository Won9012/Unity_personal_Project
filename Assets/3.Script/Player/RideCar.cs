using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RideCar : MonoBehaviour
{
    [SerializeField] private GameObject middle;
    [SerializeField] private GameObject Ride_btn;

    private void Awake()
    {
        Ride_btn.SetActive(false);
    }
    //���� Ÿ�� ���� ��Ȳ���� ����ó�� ������ ������ ��Ȳ
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !CarManager.isRide)
        {
            Ride_btn.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Ride_btn.SetActive(false);
        
    }
}
