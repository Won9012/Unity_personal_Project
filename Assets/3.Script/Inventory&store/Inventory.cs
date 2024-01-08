using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Transform rootSlot;
    public Store store;
    public int MaxStack = 999;


    public List<Slot> slots;

    public Text[] itemCount_txt;

    public int Money = 10000;




    private void Start()
    {
        slots = new List<Slot>();
        store.Buy_items += BuyItem;
        GetSlotIdx();
        gameObject.SetActive(false);


    }

    private void GetSlotIdx()
    {
        int slotCnt = rootSlot.childCount;
        for (int i = 0; i < slotCnt; i++)
        {
            var slot = rootSlot.GetChild(i).GetComponent<Slot>();
            slot.index = i;
            slots.Add(slot);
        }
    }
    public void UpdateSlotText(Slot slot)
    {
        itemCount_txt[slot.index].text = slot.item.count.ToString();
    }

    private void Update()
    {
        for (int i = 0; i < 5; i++)
        {
         //   print($"{i}��° ���� �̸�" + slots[i].item.name + "  ����" + slots[i].item.cost+ "  ��������" + slots[i].item.count);
        }
    }

    void BuyItem(ItemProperty item, int itemCount)
    {
        var emptySlot = slots.Find(t => t.item == null || t.item.name == string.Empty);
        var SameItem = slots.Find(t => t.item != null && t.item.name == item.name && t.item.count != MaxStack);


        if (emptySlot != null && SameItem == null)
        {
            emptySlot.Setitem(item, emptySlot.index);
            emptySlot.item.count = itemCount;
            UpdateSlotText(emptySlot);
            
        }
        else
        {
            int remainingSpace = MaxStack - SameItem.item.count;
            int InputNextSlotCount = itemCount - remainingSpace;
            if (remainingSpace >= itemCount)
            {
                SameItem.item.count += itemCount;
                UpdateSlotText(SameItem);
            }
            else if (remainingSpace <= itemCount)
            {
                SameItem.item.count += remainingSpace;
                UpdateSlotText(SameItem);
                SameItem.Setitem(item, SameItem.index);
                emptySlot.item.count += InputNextSlotCount;
                emptySlot.item.sprite = SameItem.item.sprite;
                emptySlot.item.name = SameItem.item.name;
                item = emptySlot.item;
                UpdateSlotText(emptySlot);
                emptySlot.Setitem(item,emptySlot.index);
            }
        }

    }
    //���ڶ� �������� ��Ȯ������ ������ ����.
    public void GetItem(ItemProperty item)
    {
        var emptySlot = slots.Find(t => t.item == null || t.item.name == string.Empty);
        var SameItem = slots.Find(t => t.item != null && t.item.name == item.name && t.item.count != MaxStack);

        //�κ��丮�� �������� ������� 
        if(emptySlot != null && SameItem == null)
        {
            emptySlot.Setitem(item, emptySlot.index);
            emptySlot.item.count++;
            UpdateSlotText(emptySlot);
        }
        else
        {
            //�κ��丮�� �������� ���� ���
            SameItem.Setitem(item, SameItem.index);
            SameItem.item.count++;
            UpdateSlotText(SameItem);
        }
        
    }


    //swap�� �ʿ��Ѱ�?
    //1.����� �ԷµǸ� Ÿ�� ���԰� �巡�� ������ ��ġ�� �ٲܰ�
    //2.List�� �ִ� ���������� ������ �ٰ�.
    //3.�ٲ� �δ콺 ������ �������ٰ� => �������� �߰��� ������ �̻��� ��ġ�� ���ŵǴ°��� �����ϱ� ����.
    public void SwapSlots(int indexA, int indexB)
    {
        Slot slotA = slots[indexA];
        Slot slotB = slots[indexB];

        ItemProperty AItem = slotA.item;
        ItemProperty BItem = slotB.item;

        ItemProperty temp = AItem;

        slotA.item = BItem;
        slotB.item = temp;

        StartCoroutine(GetImg(slotA, slotB,indexA,indexB));
    }

    //���� ���Ƴ��� ���� �����Ӵ����� ����Ǽ� �����°� ����
    IEnumerator GetImg(Slot slotA, Slot slotB, int indexA, int indexB)
    {
        yield return new WaitForSeconds(0.1f);
        slotA.image = slotA.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        slotB.image = slotB.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        Text temp = itemCount_txt[indexA];
        itemCount_txt[indexA] = itemCount_txt[indexB];
        itemCount_txt[indexB] = temp;
    }


    public void UseItem()
    {

    }




}
