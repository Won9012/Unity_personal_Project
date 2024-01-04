using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerClickHandler
{
    public enum SlotType
    {
        Store, Inventory,Tools
    }

    public SlotType slotType;

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
                    // ù ��° �ڽĿ� ����
                    Transform firstChildTransform = parentTransform.GetChild(0);
                    GameObject firstChildGameObject = firstChildTransform.gameObject;
                    firstChildGameObject.name = item.name;
                }
                else
                {
                   print("�θ� �ڽ��� �����ϴ�.");
                }
                
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DragDrop DragItem = dropped.GetComponent<DragDrop>();

/*        //�巡�׿� ����� ���� ��ġ�� ���� �ִ� �̹��� 
        Image targetSlotImg = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        //�巡���ϰ� �ִ³༮�� �̹���
        Image DragImg = DragItem.image;
*/
        //�巡���� �������� Ÿ�� ������ ��ġ�� �����۾�
        DragItem.parentAfterDrag = transform;
        //�巡�� ������ �̹��� �ʱ�ȭ
       // DragItem.image = targetSlotImg; 

        


        if (gameObject.transform.GetChild(0).gameObject != null)
        {
            //���ҵ� Ÿ�ٽ����� ��ġ ����
            gameObject.transform.GetChild(0).gameObject.transform.SetParent(DragItem.originTransform);
            //���ҵ� Ÿ�ٽ����� �̹��� �ʱ�ȭ
    //        targetSlotImg = DragImg;
        }
        int thisSlotIndex = transform.GetSiblingIndex();
        int originSlotIndex = DragItem.originTransform.GetSiblingIndex();
        inventory.SwapSlots(thisSlotIndex, originSlotIndex);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      //  print(gameObject.name);
    }

    //�κ��丮���� �������� �������� ������ �޾��ٰ�@
    //�κ��丮���� ���������� �δ콺��ȣ�� �����ͼ� �ϰ�
    //�����Կ��� ������ ���� �ش�������� �ִٸ� ���� ���� �δ콺��ȣ(�κ��丮 ���� �տ��ִ³༮�� �켱���)
    public void OnPointerClick(PointerEventData eventData)
    {
        if(slotType == SlotType.Inventory)
        {
            
            print(gameObject.name);
            foreach (var slot in inventory.slots)
            {
           //     print(slot.item.name);
             //   print(slot.item.count);
            }
        }

    }
}
