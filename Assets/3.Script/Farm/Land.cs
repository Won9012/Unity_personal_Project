using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTraker
{
    [SerializeField] Inventory inventory;
    //���� ���¿� ���� ��縦 �Ұ�
    public enum LandStatus
    {
        Grass,Farmland,waterd
    }

    public LandStatus landStatus;

    public Material grassMat, farmLandMat, wateredMat;

    new Renderer renderer;

    public GameObject select;

    //���Ѹ� Ÿ���� �ð��� ������ �ٽ� farm���� �ٲ��ٰ�..
    GameTimeStamp timeWatered;


    [Header("Crops")]
    public GameObject cropPrefab;
    //���� ���� �ɾ��� �۹�
    CropBehaviour cropPlanted = null;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out renderer);
        //�ʱ� �� ����
        SwitchLandStatus(LandStatus.Grass);
        Select(false);

        TimeManager.Instance.ResgisterTracker(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchLandStatus(LandStatus statusToSwich)
    {
        landStatus = statusToSwich;
        Material materialToSwich = grassMat;
        switch (statusToSwich)
        {
            case LandStatus.Grass:
                materialToSwich = grassMat;
                break;
            case LandStatus.Farmland:
                materialToSwich = farmLandMat;
                break;
            case LandStatus.waterd:
                materialToSwich = wateredMat;
                timeWatered =  TimeManager.Instance.GetGameTimestamp();
                break;
            default:
                break;
        }

        renderer.material = materialToSwich;
    }

    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

    public void Interact(Tools.ToolType toolType)
    {
        //interation
     //   SwitchLandStatus(LandStatus.Farmland);

        switch (toolType)
        {
            case Tools.ToolType.Axe:
                break;
            case Tools.ToolType.Pick:
                SwitchLandStatus(LandStatus.Farmland);
                break;
            case Tools.ToolType.Hoe:
                break;
            case Tools.ToolType.Water:
                if (landStatus != LandStatus.Farmland) return;
                SwitchLandStatus(LandStatus.waterd);
                break;
            default:
                break;
        }
        //��� ���·� �غ� äũ �Ұ� , ������ ���¸� üũ �� �ʿ�� ����
        //return;
        //0.�κ��丮�� ������ �ִ��� Ȯ�� �ϰ�, ������ �������� �����ϱ�
        //1.Hoe�� ����־�� ������ ���� �� ����
        //2.���� ���°� ��� or ������ �̿��߸� ���� �� ����
        //3.�������� ���� �۹��� ������� ������ x


        //Todo : 
        //       ��ġ�� ������ �����̸� ����Ʈ << (����o)
        //       �����Կ��� ���콺�� �����ų�, ����Ű�� ������ ��� ���콺�� ��ġ�� ��ġ�� �� �ִ� ������ ��ġ (Raycast Ȱ��ȭ)
        //       ���Կ��� �������� �������� ȭ��� ���̰��ϰ�, �ѹ��� ������ ��ġ, esc�� ������ ��� 


        print(inventory.slots[2].item.name);

        if (toolType == Tools.ToolType.Hoe && landStatus != LandStatus.Grass && cropPlanted == null)
        {
            print("������?");
            GameObject cropObject = Instantiate(cropPrefab, transform);
            cropObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            //�۹��� �ڶ�� �Ҵ����ֱ�
            cropPlanted = cropObject.GetComponent<CropBehaviour>();
            //cropPlanted.Plant();


        }
        
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        //24�ð��Ŀ� ���ʱ�ȭ
        if(landStatus == LandStatus.waterd)
        {
            int hoursElasped =  GameTimeStamp.CompareTimestamps(timeWatered, timeStamp);


            //���� �ָ� �۹��� ������ �ڶ󵵷� �ϱ�
            if(cropPlanted != null)
            {
                cropPlanted.Grow();
            }


            if(hoursElasped > 23)
            {
                //24�ð��Ŀ� �� ����� 
                SwitchLandStatus(LandStatus.Farmland);
            }
        }
    }
}
