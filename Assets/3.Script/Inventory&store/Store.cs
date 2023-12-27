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
                //템이 없는부분은 클릭꺼주기
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

        // 아이템 정보를 BuyUI 패널에 표시
        onBuyUI();
    }

    public void ConfirmPurchase()
    {
        // 선택한 아이템을 인벤토리에 추가하고 UI 갱신
        if (selectedStoreItem != null)
        {
            Buy_items(selectedStoreItem, GetItemCountFromUser());
            selectedStoreItem = null;  // 선택한 아이템 초기화
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
            // 현재 포커스된 객체의 자식들에서 Image 컴포넌트 찾기
            Image[] images = selectedObject.GetComponentsInChildren<Image>();
            // 두 번째 Image 컴포넌트 확인
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
        GetItemCountFromUser(); // 999이상 표기안하려면 여기다..
    }

    public void Buying_Item(Slot slot)
    {
        if (Buy_items != null && GetItemCountFromUser() > 0f)
        {
            Buy_items(slot.item, GetItemCountFromUser());
            print("뭐사냐@@@ : " + slot.item.name);
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
