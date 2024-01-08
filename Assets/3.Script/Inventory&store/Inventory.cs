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
         //   print($"{i}번째 슬롯 이름" + slots[i].item.name + "  가격" + slots[i].item.cost+ "  보유갯수" + slots[i].item.count);
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
    //다자란 아이템을 수확했을때 아이템 셋팅.
    public void GetItem(ItemProperty item)
    {
        var emptySlot = slots.Find(t => t.item == null || t.item.name == string.Empty);
        var SameItem = slots.Find(t => t.item != null && t.item.name == item.name && t.item.count != MaxStack);

        //인벤토리에 아이템이 없을경우 
        if(emptySlot != null && SameItem == null)
        {
            emptySlot.Setitem(item, emptySlot.index);
            emptySlot.item.count++;
            UpdateSlotText(emptySlot);
        }
        else
        {
            //인벤토리에 아이템이 있을 경우
            SameItem.Setitem(item, SameItem.index);
            SameItem.item.count++;
            UpdateSlotText(SameItem);
        }
        
    }


    //swap시 필요한것?
    //1.드랍이 입력되면 타겟 슬롯과 드래깅 슬롯의 위치를 바꿀것
    //2.List에 있는 슬롯정보를 갱신해 줄것.
    //3.바뀐 인댁스 정보를 갱신해줄것 => 아이템을 추가로 샀을때 이상한 위치에 구매되는것을 방지하기 위함.
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

    //뺏다 갈아끼는 순간 프레임단위로 실행되서 씹히는것 방지
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
