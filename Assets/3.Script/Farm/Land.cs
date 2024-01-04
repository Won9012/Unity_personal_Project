using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTraker
{
    [SerializeField] Inventory inventory;
    //땅의 상태에 따라 농사를 할것
    public enum LandStatus
    {
        Grass,Farmland,waterd
    }

    public LandStatus landStatus;

    public Material grassMat, farmLandMat, wateredMat;

    new Renderer renderer;

    public GameObject select;

    //물뿌린 타일은 시간이 지나면 다시 farm으로 바꿔줄것..
    GameTimeStamp timeWatered;


    [Header("Crops")]
    public GameObject cropPrefab;
    //현재 땅에 심어진 작물
    CropBehaviour cropPlanted = null;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out renderer);
        //초기 땅 설정
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
        //장비 상태로 준비를 채크 할것 , 씨앗의 상태를 체크 할 필요는 없음
        //return;
        //0.인벤토리에 씨앗이 있는지 확인 하고, 씨앗이 있을때만 생성하기
        //1.Hoe를 들고있어야 씨앗을 심을 수 있음
        //2.땅의 상태가 농사 or 젖은땅 이여야만 심을 수 있음
        //3.심으려는 땅에 작물이 없어야함 있으면 x


        //Todo : 
        //       설치가 가능한 지역이면 이팩트 << (구현o)
        //       퀵슬롯에서 마우스로 누르거나, 단축키를 눌렀을 경우 마우스의 위치에 설치할 수 있는 아이템 설치 (Raycast 활성화)
        //       슬롯에서 눌렀을때 아이템이 화면상에 보이게하고, 한번더 누르면 설치, esc를 누르면 취소 


        print(inventory.slots[2].item.name);

        if (toolType == Tools.ToolType.Hoe && landStatus != LandStatus.Grass && cropPlanted == null)
        {
            print("들어오나?");
            GameObject cropObject = Instantiate(cropPrefab, transform);
            cropObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            //작물이 자라게 할당해주기
            cropPlanted = cropObject.GetComponent<CropBehaviour>();
            //cropPlanted.Plant();


        }
        
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        //24시간후에 땅초기화
        if(landStatus == LandStatus.waterd)
        {
            int hoursElasped =  GameTimeStamp.CompareTimestamps(timeWatered, timeStamp);


            //물을 주면 작물이 빠르게 자라도록 하기
            if(cropPlanted != null)
            {
                cropPlanted.Grow();
            }


            if(hoursElasped > 23)
            {
                //24시간후에 땅 말라라 
                SwitchLandStatus(LandStatus.Farmland);
            }
        }
    }
}
