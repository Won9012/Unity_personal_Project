using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerClickHandler
{
    Land land;

    public GameObject cropObject;
    public static bool isItemClicked = false;

    public enum SlotType
    {
        Store, Inventory,Tools
    }

    public SlotType slotType;

    [HideInInspector]
    public ItemProperty item;
    public Image image;
    public Store store;
    public GameObject crop_Obj;


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
    private void Update()
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
                crop_Obj = item.cropPrefab;
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

    public void OnPointerEnter(PointerEventData eventData)
    {
      //  print(gameObject.name);
    }
    //인벤토리에서 아이템을 눌렀을때 정보를 받아줄것@
    //인벤토리에서 눌렀을때는 인댁스번호를 가져와서 하고
    //퀵슬롯에서 눌렀을 때는 해당아이템이 있다면 가장 낮은 인댁스번호(인벤토리 가장 앞에있는녀석을 우선사용)
    public void OnPointerClick(PointerEventData eventData)
    {
        if(slotType == SlotType.Inventory)
        {
            //우선 클릭했을때 아이템이 있으면 마우스포인터에 띄워주고 , 없으면 리턴
            if (inventory.slots[index].item.count >= 0)
            {
                isItemClicked = true;
                print(inventory.slots[index].item.name);
                cropObject = Instantiate(inventory.slots[index].item.cropPrefab, transform);
                //클릭후 땅에 클릭하면 아이템 생성은 Land에서 해줌
                //여기서는 인벤토리 슬롯을 눌렀을때 그 슬롯에 씨앗 아이템이 있다면 화면에 띄워줄것

                if (Input.GetKeyDown(KeyCode.Escape)) isItemClicked = false;

            }
        }
        else
        {
            isItemClicked = false; 
        }

    }

    public IEnumerator MovePrefab()
    {
        yield return new WaitForSeconds(0.1f);
        while (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Escape))
        {

        }
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // 임의의 z 값 설정
        cropObject.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }


/*    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            OnInteractableHit(hit);
}*/

}
