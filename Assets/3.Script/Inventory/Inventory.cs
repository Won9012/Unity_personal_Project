using System;
using UnityEngine;

public class Inventory : ScriptableObject, ItemContainer
{
    private ItemSlot[] itemSlots = new ItemSlot[20];

    public Action OnItemsUpdate = delegate { };

    public ItemSlot GetSlotByIndex(int index) => itemSlots[index];
    public ItemSlot AddItem(ItemSlot itemSlot)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].item != null)
            {
                if(itemSlots[i].item == itemSlot.item)
                {
                    int slotRemainingSpace =
                        itemSlots[i].item.MaxStack - itemSlots[i].quantity;
                    if(itemSlot.quantity <= slotRemainingSpace)
                    {
                        itemSlots[i].quantity += itemSlot.quantity;

                        itemSlot.quantity = 0;

                        //invoke
                        OnItemsUpdate.Invoke();

                        return itemSlot;
                    }
                    else if(slotRemainingSpace >0)
                    {
                        itemSlots[i].quantity += slotRemainingSpace;

                        itemSlot.quantity -= slotRemainingSpace;
                    }
                }
            }
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].item == null)
            {
                if(itemSlot.quantity <= itemSlot.item.MaxStack)
                {
                    itemSlots[i] = itemSlot;


                    itemSlot.quantity = 0;
                    //invoke
                    OnItemsUpdate.Invoke();

                    return itemSlot;
                }
                else
                {
                    itemSlots[i] = new ItemSlot(itemSlot.item, itemSlot.item.MaxStack);

                    itemSlot.quantity -= itemSlot.item.MaxStack;
                }
            }
        }
        //invoke
        OnItemsUpdate.Invoke();
        return itemSlot;
    }

    public int GetTotalQuantity(InventoryItem item)
    {
        int totalCount = 0;

        foreach (ItemSlot itemSlot in itemSlots)
        {
            if (itemSlot.item == null) continue;
            if (itemSlot.item != item) continue;
            totalCount += itemSlot.quantity;
        }

        return totalCount;
    }

    public bool HasItem(InventoryItem item)
    {
        foreach(ItemSlot itemSlot in itemSlots)
        {
            if (itemSlot.item == null) continue;
            if (itemSlot.item != item) continue;
            return true;
        }
        return false; 
    }

    //아이템 제거
    public void RemoveAt(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex > itemSlots.Length - 1) return;

        itemSlots[slotIndex] = new ItemSlot();

        //invoke
        OnItemsUpdate.Invoke();
    }

    public void RemoveItem(ItemSlot itemSlot)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].item != null)
            {
                if (itemSlots[i].item == itemSlot.item)
                {
                    if(itemSlots[i].quantity < itemSlot.quantity)
                    {
                        itemSlot.quantity -= itemSlots[i].quantity;

                        itemSlots[i] = new ItemSlot();
                    }
                    else
                    {
                        itemSlots[i].quantity -= itemSlot.quantity;

                        if(itemSlots[i].quantity == 0)
                        {
                            itemSlots[i] = new ItemSlot();
                            //invoke
                            OnItemsUpdate.Invoke();

                            return;
                        }
                    }
                }
            }
        }
    }

    //아이템 스왑
    public void Swap(int indexOne, int indexTwo)
    {
        ItemSlot firstSlot = itemSlots[indexOne];
        ItemSlot secondSlot = itemSlots[indexTwo];
        if (firstSlot == secondSlot) return;

        //바꾸려는 슬롯이 비어있지 않을경우 
        if(secondSlot.item != null)
        {
            //같은 아이템일경우 합칠것
            if(firstSlot.item == secondSlot.item)
            {
                int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;

                if(firstSlot.quantity <= secondSlotRemainingSpace)
                {
                    itemSlots[indexTwo].quantity += firstSlot.quantity;
                    //슬롯이 비었기때문에 정보를 초기화해줄것
                    itemSlots[indexOne] = new ItemSlot();

                    //invoke
                    OnItemsUpdate.Invoke();

                    return;
                }
            }
        }
        itemSlots[indexOne] = secondSlot;
        itemSlots[indexTwo] = firstSlot;

        //invoke
        OnItemsUpdate.Invoke();
    }
}
