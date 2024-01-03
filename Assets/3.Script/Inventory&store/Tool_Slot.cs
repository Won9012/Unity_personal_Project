using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tool_Slot : MonoBehaviour
{
    ItemData itemToDisplay;

    public Image itemDisplay_img;

    public void Display(ItemData itemToDisplay)
    {
        if(itemToDisplay != null)
        {
            itemDisplay_img.sprite = itemToDisplay.thumnail;
            this.itemToDisplay = itemToDisplay;
            itemDisplay_img.gameObject.SetActive(true);
            return;
        }

        itemDisplay_img.gameObject.SetActive(false);
    }
}
