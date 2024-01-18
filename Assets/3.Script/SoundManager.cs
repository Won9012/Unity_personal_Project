using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public AudioSource bgmSource; // ��� ������ ����� AudioSource
    public AudioSource sfxSource; // ȿ������ ����� AudioSource

    public AudioClip BGM;
    // �ټ��� ȿ������ ������ �� �ִ� ��ųʸ�
    public AudioClip CarMove;
    public AudioClip Walk;
    public AudioClip Harvest;
    public AudioClip Watering;
    public AudioClip MakeBackpack;


    public void Play_BGM()
    {

    }

}
