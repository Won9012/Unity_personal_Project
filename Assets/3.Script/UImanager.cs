using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager Instance { get; private set; }
    [Header("Inventory System")]
    public Tool_Slot[] tool_Slots;
    public Tool_Slot[] itemSlots;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        
    }

    public void RenderInventory()
    {
        ItemData[] toolSlots =  InventoryManager.Instance.tools;
        ItemData[] toolItemSlots =  InventoryManager.Instance.items;

        RenderToolSlots(toolSlots,tool_Slots);
        RenderToolSlots(toolSlots, itemSlots);
    }

    void RenderToolSlots(ItemData[] slots, Tool_Slot[] uiSlots)
    {
        for (int i = 0; i < uiSlots.Length; i++)
        {
            uiSlots[i].Display(slots[i]);
        }
    }
}
