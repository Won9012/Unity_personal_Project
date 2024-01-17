using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryData
{
    public List<SlotData> slotDataList = new List<SlotData>();
    public int Money;
}

[System.Serializable]
public class SlotData
{
    public string itemName;
    public int itemCount;
    public Sprite itemSprite;
}

public class Inventory : MonoBehaviour
{
    private static Inventory _instance;

    public static Inventory instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Inventory>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("Inventory");
                    _instance = obj.AddComponent<Inventory>();
                }
            }
            return _instance;
        }
    }
    public Transform rootSlot;
    public Store store;
    public int MaxStack = 999;


    public List<Slot> slots;

    public Text[] itemCount_txt;

    public int Money = 0;
    public Text Money_txt;

    public GameObject NomoneyUI;

    private void Start()
    {
        slots = new List<Slot>();
        store.Buy_items += BuyItem;
        GetSlotIdx();
        gameObject.SetActive(false);
        Money = DataManager.instance.nowPlayer.Gold;
        Money_txt.text = DataManager.instance.nowPlayer.Gold.ToString();
        LoadInventory();
    }


    private void Update()
    {
        print(slots[0].item);
        print(slots[1]);
        print(slots[2]);
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
        if(slot.item != null)
        {
            if (slot.item.count == 0)
            {
                itemCount_txt[slot.index].text = "";
            }
            else
            {
                itemCount_txt[slot.index].text = slot.item.count.ToString();
            }
        }
    }

    public void UpdateMoneyText(int cost)
    {
        Money_txt.text = Money.ToString();
    }

    private IEnumerator Nomoney_co()
    {
        yield return new WaitForSeconds(2f);
    }

    void BuyItem(ItemProperty item, int itemCount)
    {
        if(itemCount * item.cost > Money)
        {
            //���� �����ϸ� ������ UI Ȱ��ȭ
            print(itemCount * item.cost);
            NomoneyUI.SetActive(true);
            return;
        }
        else
        {
            print(itemCount * item.cost);
            Money -= (itemCount * item.cost);
            UpdateMoneyText(Money);
            print(Money);
        }

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


    public void SaveInventory()
    {
        InventoryData inventoryData = new InventoryData();

        foreach (var slot in slots)
        {
            SlotData slotData = new SlotData
            {
                itemName = slot.item != null ? slot.item.name : "",
                itemCount = slot.item != null ? slot.item.count : 0,
                itemSprite = slot.item != null ? slot.item.sprite : null
            };
            inventoryData.slotDataList.Add(slotData);
        }

        inventoryData.Money = Money;

        string data = JsonUtility.ToJson(inventoryData);
        File.WriteAllText(Application.persistentDataPath + "/save_inventory.json", data);
    }

    public void LoadInventory()
    {
        string filePath = Application.persistentDataPath + "/save_inventory.json";

        if (File.Exists(filePath))
        {
            string data = File.ReadAllText(filePath);
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(data);

            for (int i = 0; i < Mathf.Min(slots.Count, inventoryData.slotDataList.Count); i++)
            {
                SlotData slotData = inventoryData.slotDataList[i];
                ItemProperty item = new ItemProperty
                {
                    name = slotData.itemName,
                    count = slotData.itemCount,
                    sprite = slotData.itemSprite
                };
                slots[i].Setitem(item, i);
                UpdateSlotText(slots[i]);
            }

            Money = inventoryData.Money;
            Money_txt.text = Money.ToString();
        }
        else
        {
            Debug.Log("No inventory data found. Initializing...");
            // ������ ���� ��� �ʱ�ȭ �ڵ带 �߰��ϸ� �˴ϴ�.
        }
    }




}

