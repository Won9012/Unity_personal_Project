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

    public ItemProperty() {
        name = string.Empty;
        sprite = null;
        cost = 0;
        count = 0;
    }
    public ItemProperty(ItemProperty item)
    {
        name = item.name;
        sprite = item.sprite;
        cost = item.cost;
        count = item.count;
    }
}
