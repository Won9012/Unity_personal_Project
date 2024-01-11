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
    //차를 타지 않은 상황에서 차근처에 접근을 했을때 상황
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
