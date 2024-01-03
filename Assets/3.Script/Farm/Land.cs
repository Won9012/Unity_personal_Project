using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
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


    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out renderer);
        //초기 땅 설정
        SwitchLandStatus(LandStatus.Grass);
        Select(false);
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
                // Handle interaction for Axe tool type (if needed)
                break;
            case Tools.ToolType.Pick:
                // Handle interaction for Pick tool type (if needed)
                break;
            case Tools.ToolType.Hoe:
                // Switch land status to Farmland when interacting with Hoe
                SwitchLandStatus(LandStatus.Farmland);
                break;
            case Tools.ToolType.Water:
                // Handle interaction for Water tool type (if needed)
                break;
            default:
                break;
        }
    }
}
