using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RideCar : MonoBehaviour
{
    [SerializeField] private GameObject middle;
    [SerializeField] private GameObject Ride_btn;


    //���� Ÿ�� ���� ��Ȳ���� ����ó�� ������ ������ ��Ȳ
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
