using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tools : MonoBehaviour
{
    ItemData itemToDisplay;
    public ItemBuffer itemBuffer;
    public Image itemDisplay_img;

    public Transform slotRoot;

    private List<Slot> slots;

    public Button[] buttons;


    public Image StatusImage;

    public GameObject[] Tool_items;

    public Sprite BackPack;

    public enum ToolType
    {
        Empty,Axe, Pick, Water, Hoe , EquipedBackpack
    }

    public static ToolType toolType = ToolType.Empty;

    private void Start()
    {
        ToolSetting();
        GetSlotIdx();
    }
    public void ToolSetting()
    {
        slots = new List<Slot>();

        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();
            if (i < itemBuffer.items.Count)
            {
                slot.Setitem(itemBuffer.items[i], slot.index);
            }
            else
            {
                slot.GetComponent<Button>().interactable = false;
            }
            slots.Add(slot);
        }
    }

    private void GetSlotIdx()
    {
        int slotCnt = slotRoot.childCount;
        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();
            slot.index = i;
            slots.Add(slot);
        }
    }

    //순서는 도끼, 곡괭이,삽, 분무기
    public void Get_Axe()
    {
        StatusImage.sprite = slots[0].item.sprite;
        StatusImage.enabled = true;
        toolType = ToolType.Axe;
        for (int i = 0; i < Tool_items.Length; i++)
        {
            if(Tool_items[i].name == "Axe")
            {
                Tool_items[i].SetActive(true);
            }
            else
            {
                Tool_items[i].SetActive(false);
            }
        }
    }
    public void Get_pick()
    {
        StatusImage.sprite = slots[1].item.sprite;
        StatusImage.enabled = true;
        toolType = ToolType.Pick;
        for (int i = 0; i < Tool_items.Length; i++)
        {
            if (Tool_items[i].name == "Pick")
            {
                Tool_items[i].SetActive(true);
            }
            else
            {
                Tool_items[i].SetActive(false);
            }
        }
    }
    public void Get_hoe()
    {
        StatusImage.sprite = slots[2].item.sprite;
        StatusImage.enabled = true;
        toolType = ToolType.Hoe;
        for (int i = 0; i < Tool_items.Length; i++)
        {
            if (Tool_items[i].name == "Hoe")
            {
                Tool_items[i].SetActive(true);
            }
            else
            {
                Tool_items[i].SetActive(false);
            }
        }
    }
    public void Get_Water()
    {
        StatusImage.sprite = slots[3].item.sprite;
        StatusImage.enabled = true;
        toolType = ToolType.Water;
        for (int i = 0; i < Tool_items.Length; i++)
        {
            if (Tool_items[i].name == "Water")
            {
                Tool_items[i].SetActive(true);
            }
            else
            {
                Tool_items[i].SetActive(false);
            }
        }
    }

    public void Get_Backpack()
    {
        //무역 아이템을 얻었을 때 백팩 활성화 및 이미지 교체작업해줄것
    }
}
