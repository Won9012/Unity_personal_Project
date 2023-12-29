using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    [HideInInspector]
    public ItemProperty item;
    public Image image;
    public Store store;
    
    public Button sellbtn;
    
    [SerializeField]private Inventory inventory;
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


    public void Setitem(ItemProperty newitem,int index)
    {
       // this.item = item;

        item = newitem;

        if(item == null)
        {
            image.enabled  = false;
            gameObject.name = "Empty";
        }
        else
        {
            if(item != null)
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

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragDrop DragItem = dropped.GetComponent<DragDrop>();

/*        //드래그엔 드랍후 놓는 위치에 원래 있던 이미지 
        Image targetSlotImg = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        //드래그하고 있는녀석의 이미지
        Image DragImg = DragItem.image;
*/
        //드래그한 아이템을 타겟 슬롯의 위치에 놓는작업
        DragItem.parentAfterDrag = transform;
        //드래그 아이템 이미지 초기화
       // DragItem.image = targetSlotImg; 

        


        if (gameObject.transform.GetChild(0).gameObject != null)
        {
            //스왑된 타겟슬롯의 위치 설정
            gameObject.transform.GetChild(0).gameObject.transform.SetParent(DragItem.originTransform);
            //스왑된 타겟슬롯의 이미지 초기화
    //        targetSlotImg = DragImg;
        }
        int thisSlotIndex = transform.GetSiblingIndex();
        int originSlotIndex = DragItem.originTransform.GetSiblingIndex();
        inventory.SwapSlots(thisSlotIndex, originSlotIndex);
    }

}
