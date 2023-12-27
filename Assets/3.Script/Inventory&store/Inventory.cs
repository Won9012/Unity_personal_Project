using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform rootSlot;
    public Store store;
    public int MaxStack = 999;


    private List<Slot> slots;

    public Text[] itemCount_txt;

    public int Money = 10000;

    private void Start()
    {
        slots = new List<Slot>();

        int slotCnt = rootSlot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = rootSlot.GetChild(i).GetComponent<Slot>();
            slot.index = i;
            slots.Add(slot);
        }

        store.Buy_items += BuyItem;

    }

    private void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            print(i+1+ " 번째 슬롯이름 " + slots[i].item.name);
            print(i+1 + " 번째 갯수 " + slots[i].item.count);
        }
    }
    void UpdateSlotText(Slot slot)
    {
        itemCount_txt[slot.index].text = slot.item.count.ToString();
    }

    void BuyItem(ItemProperty item, int itemCount)
    {
        var emptySlot = slots.Find(t => t.item == null || t.item.name == string.Empty);
        var SameItem = slots.Find(t => t.item != null && t.item.name == item.name && t.item.count != MaxStack);


        if (emptySlot != null && SameItem == null)
        {

            emptySlot.Setitem(item);
            emptySlot.item.count = itemCount;
            UpdateSlotText(emptySlot);
        }
        else
        {
            //999개 - 현재 인벤토리 보유량  = 남는양
            int remainingSpace = MaxStack - SameItem.item.count;
            int InputNextSlotCount = itemCount - remainingSpace;
            //남은 저장공간이 들어온 아이템 갯수보다 많다면 
            if (remainingSpace >= itemCount)
            {
                SameItem.item.count += itemCount;
                UpdateSlotText(SameItem);
            }//남은 저장공간이 들어온 아이템 갯수보다 적다면 500개가 들어왔다면
            else if (remainingSpace <= itemCount)
            {
                // 600개가 있고 400개가 들어왔으면  399개가 remainingSpace  나머지가 1개 
                SameItem.item.count += remainingSpace;
                //SameItem.item.index++;
                UpdateSlotText(SameItem);
                SameItem.Setitem(item);
                //아이템이 뒤로 추가되게 하기위해서 인댁스관리
                emptySlot.item.count += InputNextSlotCount;
                emptySlot.item.sprite = SameItem.item.sprite;
                emptySlot.item.name = SameItem.item.name;
                item = emptySlot.item;
                
                UpdateSlotText(emptySlot);
                print(emptySlot.item);

                emptySlot.Setitem(item);


            }
        }

    }
}
