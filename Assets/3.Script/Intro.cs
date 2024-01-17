using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class Intro : MonoBehaviour
{
    public GameObject Creat;
    public Text[] slotText;
    public Text newPlayerName;

    bool[] savefile = new bool[3];

    private void Start()
    {
        for (int i = 0; i < 3; i++) 
        {
            if(File.Exists(DataManager.instance.path + $"{i}"))
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();
                slotText[i].text = DataManager.instance.nowPlayer.name;
            }
            else
            {
                slotText[i].text = "비어있음";
            }
        }
        DataManager.instance.DataClear();  
    }


    //슬롯이 3개인대 알맞게 불러오는방법
    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;
        //1. 저장된 데이터가 X
        if (savefile[number])
        {
            DataManager.instance.LoadData();
            GoGame();
        }
        else
        {
            IntroCreat();
        }
    }
    public void IntroCreat()
    {
        Creat.gameObject.SetActive(true);
    }

    public void GoGame()
    {
        if (!savefile[DataManager.instance.nowSlot])
        {
            DataManager.instance.nowPlayer.name = newPlayerName.text;
            DataManager.instance.SaveData();
            Debug.Log("플레이어 데이터가 저장되었습니다. 플레이어 이름: " + DataManager.instance.nowPlayer.name);
        }
        SceneManager.LoadScene(1);
    }
}
