using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stall : MonoBehaviour
{

    //todo :  Ŭ���� �־��� Image[] �۾� �� �߰�.
    //todo :  �� ��ư �Լ��� �ϼ�.
    //



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



    //Stall ���� ������ ������ ������ ��ư��.

    public void CreatBackPack_Con()
    {

    }
    public void CreatBackPack_Egg()
    {

    }
    public void CreatBackPack_Pumpkin()
    {

    }
    public void CreatBackPack_Tomato()
    {

    }
    public void CreatBackPack_Turnip()
    {

    }




}

    
