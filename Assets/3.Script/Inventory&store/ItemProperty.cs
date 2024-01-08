using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemProperty
{
    public string name;
    public Sprite sprite;
    public int cost;
    public int count = 0;
    public string Description;

    public int daysToGrow;
    public GameObject cropPrefab;

    public ItemType itemType;
}

public enum ItemType
{
    SEED, Equipment, Harvestable
}

