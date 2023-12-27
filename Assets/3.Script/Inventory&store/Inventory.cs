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
            print(i+1+ " ��° �����̸� " + slots[i].item.name);
            print(i+1 + " ��° ���� " + slots[i].item.count);
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
            //999�� - ���� �κ��丮 ������  = ���¾�
            int remainingSpace = MaxStack - SameItem.item.count;
            int InputNextSlotCount = itemCount - remainingSpace;
            //���� ��������� ���� ������ �������� ���ٸ� 
            if (remainingSpace >= itemCount)
            {
                SameItem.item.count += itemCount;
                UpdateSlotText(SameItem);
            }//���� ��������� ���� ������ �������� ���ٸ� 500���� ���Դٸ�
            else if (remainingSpace <= itemCount)
            {
                // 600���� �ְ� 400���� ��������  399���� remainingSpace  �������� 1�� 
                SameItem.item.count += remainingSpace;
                //SameItem.item.index++;
                UpdateSlotText(SameItem);
                SameItem.Setitem(item);
                //�������� �ڷ� �߰��ǰ� �ϱ����ؼ� �δ콺����
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
