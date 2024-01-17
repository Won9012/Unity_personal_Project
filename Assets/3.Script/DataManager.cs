using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//�����
// 1. ������ ������ 
// 2. ������ ���̽����� ��ȯ
// 3. �ܺο� ����


//�ҷ�����
//1.�ܺ� ����  ���̽��� ������
//2. ���̽��� ���������׷� ��ȯ
//3. �ҷ��� �����͸� ���
public class PlayerData
{
    public string name;
    public int Gold = 100000;
    public int Item = -1;
    public Vector3 position;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();

    public string path;
    public int nowSlot;

    private void Awake()
    {
        #region �̱���
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/save";
        print(path);
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data);
    }
    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = null;
        nowPlayer = new PlayerData();
    }
}
