using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, ITimeTraker
{
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
                break;
            case Tools.ToolType.Hoe:
                SwitchLandStatus(LandStatus.Farmland);
                break;
            case Tools.ToolType.Water:
                if (landStatus != LandStatus.Farmland) return;
                SwitchLandStatus(LandStatus.waterd);
                break;
            default:
                break;
        }
    }

    public void ClockUpdate(GameTimeStamp timeStamp)
    {
        //24시간후에 땅초기화
        if(landStatus == LandStatus.waterd)
        {
            int hoursElasped =  GameTimeStamp.CompareTimestamps(timeWatered, timeStamp);
            print(hoursElasped);

            if(hoursElasped > 23)
            {
                //24시간후에 땅 말라라 
                SwitchLandStatus(LandStatus.Farmland);
            }
        }
    }
}
