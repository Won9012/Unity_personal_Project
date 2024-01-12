using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    ItemProperty seedToGrow;

    public Inventory inventory;

    [Header("Seed LifeTime")]
    public GameObject seed;
    public GameObject seedling;
    public GameObject harvestabe; // 수확가능
    public Collider Triger_Colider;
    public ItemBuffer Haves_ItemBuffer;

    //아이템을 심었을때, 프리팹을 생성
    //프리팹구조는 
    //Crop (부모)
    //seed , seeding , harvestable(자식)
    //자식에서 순서에 맞게 진행할것.
    //입력받는 seedToGrow 는 day기반의 단위
    //따라서, 심기시작한 시점에서 

    private void Awake()
    {
        // 이게 왜 되는거지? ; => 오브젝트 순서 마지막에 있는애 찾네 
        Haves_ItemBuffer = GameObject.FindObjectOfType<ItemBuffer>(); // 정확히 찾을수 있도록 변경해야함..
        inventory = GameObject.FindObjectOfType<Inventory>();
    }

    private void Update()
    {
        // print(MaxGrowth);
        // print(growth);.
    }

    int growth;
    int MaxGrowth;

    public enum CropState
    {
        SEED, SEEDLING, HARVESTABLE
    }
    //씨앗을 심고 시간에 따라 자랄때 상태 부여
    public CropState cropState;

    public void Plant(ItemProperty seedToGrow)
    {
        //씨앗 지정
        this.seedToGrow = seedToGrow;

        //심는 작물 생성
/*        seed_prefab = Instantiate(seed_prefab, transform);
        
        //중간과정
        seedling_prefab = Instantiate(seedling_prefab, transform);
        seedling_prefab.transform.position = seed_prefab.transform.position;
        //다자란애 생성
        harvestabe_prefab = Instantiate(harvestabe_prefab, transform);
        harvestabe_prefab.transform.position = seed_prefab.transform.position;*/
        
        //시간단위 => 분단위
        int hoursToGrow = GameTimeStamp.DaysToHours(seedToGrow.daysToGrow);
        int MinuteToGrow = GameTimeStamp.HoursToMinutes(hoursToGrow);

        MaxGrowth = GameTimeStamp.DaysToHours(hoursToGrow); //테스트용 숫자
       // MaxGrowth = GameTimeStamp.HoursToMinutes(MinuteToGrow);

        SwichState(CropState.SEED);
    }

    //심으면 자라게 할꺼임
    public void GrowFast()
    {
        growth+=2;

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
        //시간이 지남에 따라 변경, 물을 줄때도 변경.
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
                Triger_Colider.enabled = true;
                break;
            default:
                break;
        }

        //현재 작물의 상태 변경
        cropState = stateToSwich;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && cropState == CropState.HARVESTABLE && Input.GetKeyDown(KeyCode.Space))
        {
             print("들어오냐?");
            for (int i = 0; i < Haves_ItemBuffer.items.Count; i++)
            {
                if(Haves_ItemBuffer.items[i].name == gameObject.transform.GetChild(2).name)
                {
                    inventory.GetItem(Haves_ItemBuffer.items[i]);
                    Destroy(gameObject);
                }
            }
            
        }
    }

}
