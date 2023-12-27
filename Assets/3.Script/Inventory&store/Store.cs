using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    public ItemBuffer itemBuffer;
    public Transform slotRoot;

    public InputField itemCountInput;
    public int inputMax = 999;
    public Text itemcount;
    public GameObject BuyUI;

    public Image Select_img;

    private List<Slot> slots;

    public Action<ItemProperty, int> Buy_items;
    // Start is called before the first frame update

    private ItemProperty selectedStoreItem;
    void Start()
    {
        StoreSeting();
    }

    public void StoreSeting()
    {
        slots = new List<Slot>();
        int slotCnt = slotRoot.childCount;

        for (int i = 0; i < slotCnt; i++)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();
            if (i < itemBuffer.items.Count)
            {
                slot.Setitem(itemBuffer.items[i]);
            }
            else
            {
                //���� ���ºκ��� Ŭ�����ֱ�
                slot.GetComponent<Button>().interactable = false;
            }
            slots.Add(slot);
        }

        foreach (var slot in slots)
        {
            slot.GetComponent<Button>().onClick.AddListener(() => OnStoreItemClick(slot));
        }
    }

    private void OnStoreItemClick(Slot slot)
    {
        selectedStoreItem = slot.item;

        // ������ ������ BuyUI �гο� ǥ��
        onBuyUI();
    }

    public void ConfirmPurchase()
    {
        // ������ �������� �κ��丮�� �߰��ϰ� UI ����
        if (selectedStoreItem != null)
        {
            Buy_items(selectedStoreItem, GetItemCountFromUser());
            selectedStoreItem = null;  // ������ ������ �ʱ�ȭ
            offBuyUI();
        }
    }

    public void onBuyUI()
    {
        BuyUI.SetActive(true);
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        itemCountInput.text = "0";

        if (selectedObject != null)
        {
            // ���� ��Ŀ���� ��ü�� �ڽĵ鿡�� Image ������Ʈ ã��
            Image[] images = selectedObject.GetComponentsInChildren<Image>();
            // �� ��° Image ������Ʈ Ȯ��
            if (images.Length >= 2)
            {
                Image secondImage = images[1];
             //   Debug.Log("Second button image: " + secondImage.sprite);
                Select_img.sprite = secondImage.sprite;
            }
        }

    }

    public void offBuyUI()
    {
        BuyUI.SetActive(false);

    }

    private void Update()
    {
        GetItemCountFromUser(); // 999�̻� ǥ����Ϸ��� �����..
    }

    public void Buying_Item(Slot slot)
    {
        if (Buy_items != null && GetItemCountFromUser() > 0f)
        {
            Buy_items(slot.item, GetItemCountFromUser());
            print("�����@@@ : " + slot.item.name);
            int itemCount = slot.item.count;
        }
        else
        {
            return;
        }
    }

    private int GetItemCountFromUser()
    {
        if (int.TryParse(itemCountInput.text, out int itemCount))
        {
            if(itemCount > inputMax)
            {
                itemCount = inputMax;
                itemCountInput.text = inputMax.ToString();
            }
            return itemCount;
        }
        else
        {
            return 0;
        }
    }
}
