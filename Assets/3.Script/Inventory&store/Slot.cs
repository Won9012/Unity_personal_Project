using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [HideInInspector]
    public ItemProperty item;
    public Image image;
    
    public Button sellbtn;
    
    private Inventory inventory;
    
    private bool clicked = false;

    public bool IsClicked()
    {
        return clicked;
    }

    public void SetClicked(bool value)
    {
        clicked = value;
    }
    private void Awake()
    {

    }


    public void Setitem(ItemProperty item)
    {
        this.item = item;

        if(item == null)
        {
            image.enabled = false;
            gameObject.name = "Empty";
        }
        else
        {
            if(item != null)
            {
                image.enabled = true;
                gameObject.name = item.name;
                image.sprite = item.sprite;
            }
        }
    }
}
