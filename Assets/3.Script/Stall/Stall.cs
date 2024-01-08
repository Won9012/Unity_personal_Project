using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stall : MonoBehaviour
{

    //todo :  클릭시 넣어줄 Image[] 작업 및 추가.
    //todo :  각 버튼 함수들 완성.
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



    //Stall 에서 누르면 아이템 제작할 버튼들.

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

    
