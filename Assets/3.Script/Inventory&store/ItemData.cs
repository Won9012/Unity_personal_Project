using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Items/Item")]
public class ItemData : ScriptableObject
{
    public string description;

    //Icon
    public Sprite thumnail;
    public GameObject gameModel;
}
