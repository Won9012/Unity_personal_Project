using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    SeedData seedToGrow;

    [Header("Seed LifeTime")]
    public GameObject seed;
    public GameObject seedling;
    public GameObject harvestabe; // 수확가능


    int growth;
    int MaxGrowth;

    public enum CropState
    {
        SEED, SEEDLING, HARVESTABLE
    }
    //씨앗을 심고 시간에 따라 자랄때 상태 부여
    public CropState cropState;

    public void Plant(SeedData seedToGrow)
    {
        //씨앗 지정
        this.seedToGrow = seedToGrow;

        //심는 작물 생성
        seedling = Instantiate(seedToGrow.seeding, transform);
        //Access the crop item data
        ItemData cropToYield = seedToGrow.cropToYield;

        //다자란애 생성
        harvestabe = Instantiate(cropToYield.gameModel, transform);

        //일단위 => 시간단위
        int hoursToGrow = GameTimeStamp.DaysToHours(seedToGrow.daysToGrow);
        //시간단위 => 분단위
        MaxGrowth = GameTimeStamp.HoursToMinutes(hoursToGrow);
        //분단위 => 초단위

        SwichState(CropState.SEED);
    }

    public void Grow()
    {
        growth++;

        //재배 시간의 반만큼 지난다면, 줄기로 변환
        if(growth >= MaxGrowth * 0.5 && cropState == CropState.SEED)
        {
            SwichState(CropState.SEEDLING);
        }


        if(growth >= MaxGrowth && cropState == CropState.SEEDLING)
        {
            SwichState(CropState.HARVESTABLE);
        }
    }

    private void SwichState(CropState stateToSwich)
    {
        //초기 상태 셋팅
        seed.SetActive(false);
        seedling.SetActive(false);
        harvestabe.SetActive(false);

        switch (stateToSwich)
        {
            case CropState.SEED:
                seed.SetActive(true);
                break;
            case CropState.SEEDLING:
                seedling.SetActive(true);
                break;
            case CropState.HARVESTABLE:
                harvestabe.SetActive(true);
                break;
            default:
                break;
        }

        //현재 작물의 상태 변경
        cropState = stateToSwich;
    }
}
