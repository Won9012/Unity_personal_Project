using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public GameObject inventory;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {

    }
    private void Update()
    {
        ToogleInventroy();
    }
    private void ToogleInventroy()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);
            }
            else
            {
                inventory.SetActive(true);
            }
        }
    }

    [Header("Tools")]
    //Tool slots
    public ItemData[] tools = new ItemData[4];
    //Tool in the player's hand
    public ItemData equippedTool = null;
    [Header("Items")]
    //Item slots
    public ItemData[] items = new ItemData[4];
    //Item in the player's hand
    public ItemData equippedItem = null;
}
