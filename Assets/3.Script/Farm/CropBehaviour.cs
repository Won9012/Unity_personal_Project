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
    public GameObject harvestabe; // ��Ȯ����
    public Collider Triger_Colider;
    public ItemBuffer Haves_ItemBuffer;

    //�������� �ɾ�����, �������� ����
    //�����ձ����� 
    //Crop (�θ�)
    //seed , seeding , harvestable(�ڽ�)
    //�ڽĿ��� ������ �°� �����Ұ�.
    //�Է¹޴� seedToGrow �� day����� ����
    //����, �ɱ������ �������� 

    private void Awake()
    {
        // �̰� �� �Ǵ°���? ; => ������Ʈ ���� �������� �ִ¾� ã�� 
        Haves_ItemBuffer = GameObject.FindObjectOfType<ItemBuffer>(); // ��Ȯ�� ã���� �ֵ��� �����ؾ���..
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
    //������ �ɰ� �ð��� ���� �ڶ��� ���� �ο�
    public CropState cropState;

    public void Plant(ItemProperty seedToGrow)
    {
        //���� ����
        this.seedToGrow = seedToGrow;

        //�ɴ� �۹� ����
/*        seed_prefab = Instantiate(seed_prefab, transform);
        
        //�߰�����
        seedling_prefab = Instantiate(seedling_prefab, transform);
        seedling_prefab.transform.position = seed_prefab.transform.position;
        //���ڶ��� ����
        harvestabe_prefab = Instantiate(harvestabe_prefab, transform);
        harvestabe_prefab.transform.position = seed_prefab.transform.position;*/
        
        //�ð����� => �д���
        int hoursToGrow = GameTimeStamp.DaysToHours(seedToGrow.daysToGrow);
        int MinuteToGrow = GameTimeStamp.HoursToMinutes(hoursToGrow);

        MaxGrowth = GameTimeStamp.DaysToHours(hoursToGrow); //�׽�Ʈ�� ����
       // MaxGrowth = GameTimeStamp.HoursToMinutes(MinuteToGrow);

        SwichState(CropState.SEED);
    }

    //������ �ڶ�� �Ҳ���
    public void GrowFast()
    {
        growth+=2;

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
        //�ð��� ������ ���� ����, ���� �ٶ��� ����.
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

        //���� �۹��� ���� ����
        cropState = stateToSwich;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && cropState == CropState.HARVESTABLE && Input.GetKeyDown(KeyCode.Space))
        {
             print("������?");
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
