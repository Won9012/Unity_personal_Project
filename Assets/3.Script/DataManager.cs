using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//저장법
// 1. 저장할 데이터 
// 2. 데이터 제이슨으로 변환
// 3. 외부에 저장


//불러오기
//1.외부 저장  제이슨을 가져옴
//2. 제이슨을 데이터형테로 변환
//3. 불러온 데이터를 사용
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
        #region 싱글톤
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
