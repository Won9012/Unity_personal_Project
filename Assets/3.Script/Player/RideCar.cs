using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RideCar : MonoBehaviour
{
    [SerializeField] private GameObject middle;
    [SerializeField] private GameObject Image;

    private void Awake()
    {
        Image.SetActive(false);
    }
    //���� Ÿ�� ���� ��Ȳ���� ����ó�� ������ ������ ��Ȳ
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !CarManager.isRide)
        {
            Image.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Image.SetActive(false);
    }
}
