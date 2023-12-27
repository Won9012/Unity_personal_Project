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
            slots.Add(slot);
        }

        store.Buy_items += BuyItem;

    }

    private void Update()
    {
        print(slots[0].item.name);
        print(slots[0].item.count);
        print(slots[1].item.name);
        print(slots[1].item.count);
        print(slots[2].item.name);
        print(slots[2].item.count);
        print(slots[3].item.name);
        print(slots[3].item.count);
    }
    void UpdateSlotText(Slot slot)
    {
        int index = slots.IndexOf(slot);
        itemCount_txt[index].text = slot.item.count.ToString();
    }

    void BuyItem(ItemProperty item, int itemCount)
    {
        var emptySlot = slots.Find(t => t.item == null || t.item.name == string.Empty);
        var SameItem = slots.Find(t => t.item != null && t.item.name == item.name);

        if (SameItem != null)
        {
            int remainingSpace = MaxStack - SameItem.item.count;

            if (remainingSpace >= itemCount)
            {
                SameItem.item.count += itemCount;
                UpdateSlotText(SameItem);
            }
            else
            {
                SameItem.item.count += remainingSpace;
                UpdateSlotText(SameItem); // 먼저 현재 슬롯을 업데이트합니다.

                // 다음 사용 가능한 슬롯을 찾아 남은 아이템을 추가합니다.
                var nextEmptySlot = slots.Find(t => t.item == null || string.IsNullOrEmpty(t.item.name));
                if (nextEmptySlot != null)
                {
                    AddToEmptySlot(item, itemCount - remainingSpace, nextEmptySlot);
                }
                else
                {
                    // 가득 찬 슬롯의 인덱스를 찾습니다.
                    int fullSlotIndex = slots.IndexOf(SameItem);

                    // 가득 찬 슬롯의 인덱스를 1 증가시킵니다.
                    fullSlotIndex = (fullSlotIndex + 1) % slots.Count;

                    // 0번 인덱스에 남은 아이템을 추가합니다.
                    AddToEmptySlot(item, itemCount - remainingSpace, slots[fullSlotIndex]);
                }
            }
        }

        if (emptySlot != null && SameItem == null)
        {
            emptySlot.Setitem(item);
            emptySlot.item.count = itemCount;
            // 해당 슬롯의 텍스트 업데이트
            UpdateSlotText(emptySlot);
        }
    }

    void AddToEmptySlot(ItemProperty item, int itemCount, Slot emptySlot)
    {
        if (emptySlot != null)
        {
            print("빈 슬롯 이름: " + emptySlot.name);
            emptySlot.Setitem(item);
            emptySlot.item.count = itemCount;
            int index = slots.IndexOf(emptySlot);
            itemCount_txt[index].text = emptySlot.item.count.ToString();
        }
        else
        {
            print("모든 슬롯이 찼습니다!");
        }
    }
}
