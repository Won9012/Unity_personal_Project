using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    SeedData seedToGrow;

    [Header("Seed LifeTime")]
    public GameObject seed;
    public GameObject seedling;
    public GameObject harvestabe; // ��Ȯ����


    int growth;
    int MaxGrowth;

    public enum CropState
    {
        SEED, SEEDLING, HARVESTABLE
    }
    //������ �ɰ� �ð��� ���� �ڶ��� ���� �ο�
    public CropState cropState;

    public void Plant(SeedData seedToGrow)
    {
        //���� ����
        this.seedToGrow = seedToGrow;

        //�ɴ� �۹� ����
        seedling = Instantiate(seedToGrow.seeding, transform);
        //Access the crop item data
        ItemData cropToYield = seedToGrow.cropToYield;

        //���ڶ��� ����
        harvestabe = Instantiate(cropToYield.gameModel, transform);

        //�ϴ��� => �ð�����
        int hoursToGrow = GameTimeStamp.DaysToHours(seedToGrow.daysToGrow);
        //�ð����� => �д���
        MaxGrowth = GameTimeStamp.HoursToMinutes(hoursToGrow);
        //�д��� => �ʴ���

        SwichState(CropState.SEED);
    }

    public void Grow()
    {
        growth++;

        //��� �ð��� �ݸ�ŭ �����ٸ�, �ٱ�� ��ȯ
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
        //�ʱ� ���� ����
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

        //���� �۹��� ���� ����
        cropState = stateToSwich;
    }
}
