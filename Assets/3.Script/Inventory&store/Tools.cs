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

    public enum ToolType
    {
        Empty,Axe, Pick, Water, Hoe
    }

    public static ToolType toolType;

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

    //¼ø¼­´Â µµ³¢, °î±ªÀÌ,»ð, ºÐ¹«±â
    public void Get_Axe()
    {
        StatusImage.sprite = slots[0].item.sprite;
        StatusImage.enabled = true;
        toolType = ToolType.Axe;

    }
    public void Get_pick()
    {
        StatusImage.sprite = slots[1].item.sprite;
        StatusImage.enabled = true;
        toolType = ToolType.Pick;
    }
    public void Get_hoe()
    {
        StatusImage.sprite = slots[2].item.sprite;
        StatusImage.enabled = true;
        toolType = ToolType.Hoe;
    }
    public void Get_Water()
    {
        StatusImage.sprite = slots[3].item.sprite;
        StatusImage.enabled = true;
        toolType = ToolType.Water;
    }
}
