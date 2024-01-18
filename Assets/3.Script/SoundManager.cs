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

    public AudioSource bgmSource; // 배경 음악을 재생할 AudioSource
    public AudioSource sfxSource; // 효과음을 재생할 AudioSource

    public AudioClip BGM;
    // 다수의 효과음을 저장할 수 있는 딕셔너리
    public AudioClip CarMove;
    public AudioClip Walk;
    public AudioClip Harvest;
    public AudioClip Watering;
    public AudioClip MakeBackpack;


    public void Play_BGM()
    {

    }

}
