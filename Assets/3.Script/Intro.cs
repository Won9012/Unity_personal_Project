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
                slotText[i].text = "�������";
            }
        }
        DataManager.instance.DataClear();  
    }


    //������ 3���δ� �˸°� �ҷ����¹��
    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;
        //1. ����� �����Ͱ� X
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
            Debug.Log("�÷��̾� �����Ͱ� ����Ǿ����ϴ�. �÷��̾� �̸�: " + DataManager.instance.nowPlayer.name);
        }
        SceneManager.LoadScene(1);
    }
}
