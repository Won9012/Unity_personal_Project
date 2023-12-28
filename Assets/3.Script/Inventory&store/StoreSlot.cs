using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSlot : MonoBehaviour
{
    [HideInInspector]
    public ItemProperty item;
    public Image image;
    public Store store;

    public Button sellbtn;

    [SerializeField] private Inventory inventory;
    public int index = 0;
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


    public void Setitem_store(ItemProperty newitem, int index)
    {
        // this.item = item;

        item = newitem;

        if (item == null)
        {
            image.enabled = false;
            gameObject.name = "Empty";
        }
        else
        {
            if (item != null)
            {
                Transform parentTransform = gameObject.transform;
                image.enabled = true;
                image.sprite = item.sprite;
                if (parentTransform.childCount > 0)
                {
                    // 첫 번째 자식에 접근
                    Transform firstChildTransform = parentTransform.GetChild(0);
                    GameObject firstChildGameObject = firstChildTransform.gameObject;
                    firstChildGameObject.name = item.name;
                }
                else
                {
                    print("부모에 자식이 없습니다.");
                }

            }
        }
    }
}

