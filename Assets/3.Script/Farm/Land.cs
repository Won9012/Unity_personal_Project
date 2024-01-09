using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Land : MonoBehaviour, ITimeTraker, IPointerClickHandler
{
    [SerializeField] Inventory inventory;
    Slot slot;

    public Vector3 LandPosition;
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
    public CropBehaviour cropPlanted = null;

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
      //  print(Slot.isItemClicked);
        LandPosition = new Vector3(transform.position.x, .08f, transform.position.z);
       // print(LandPosition);
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
        //��� �������� �ٲٴ� ����
        switch (toolType)
        {
            case Tools.ToolType.Axe:
                break;
            case Tools.ToolType.Pick:
                if (landStatus == LandStatus.waterd) return;
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

        //Slot���� Ŭ�� �̺�Ʈ ������ �޾ƿ� , �������� �����ϴ� ����.
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

        if (Input.GetMouseButtonDown(0) && Slot.isItemClicked)
        {
            if (landStatus != LandStatus.Grass && cropPlanted == null)
            {
                // GameObject cropObject = Instantiate(slot.crop_Obj, transform);
                // GameObject cropObject = Instantiate(inventory.slots[Clicked_slot.index].item.cropPrefab, transform);
                //�κ��丮���� �������� �־ ���Ⱑ ���������� �������� ���ְ�, ���� �ֽ�ȭ.
                // inventory.slots[Clicked_slot.index].item.count--;
                //inventory.UpdateSlotText(inventory.slots[Clicked_slot.index]);

             //   slot.cropObject.transform.position = new Vector3(transform.position.x, .08f, transform.position.z);
                //�۹��� �ڶ�� �Ҵ����ֱ�
             //   cropPlanted = slot.cropObject.GetComponent<CropBehaviour>();
             //   cropPlanted.Plant(inventory.slots[slot.index].item); //���� ���� �Ĺ��� �δ콺 ��ȣ�� ����.
            }
        }
        else
        {
            //todo : ������ ���� ������ ��� , �۹��� ���� �� �����ϴ� !! UI �������.
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

    public void OnPointerClick(PointerEventData eventData)
    {
        print(gameObject.name);
    }
}
