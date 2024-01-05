using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    Land land;

    private GameObject cropObject;
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


    //인벤토리에서 아이템을 눌렀을때 정보를 받아줄것@
    //인벤토리에서 눌렀을때는 인댁스번호를 가져와서 하고
    //퀵슬롯에서 눌렀을 때는 해당아이템이 있다면 가장 낮은 인댁스번호(인벤토리 가장 앞에있는녀석을 우선사용)

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotType == SlotType.Inventory)
        {
            //우선 클릭했을때 아이템이 있으면 마우스포인터에 띄워주고 , 없으면 리턴
            if (inventory.slots[index].item.count >= 0 && !isItemClicked)
            {
                isItemClicked = true;
                //print(inventory.slots[index].item.name);
                cropObject = Instantiate(inventory.slots[index].item.cropPrefab, transform);
                StartCoroutine(MovePrefab());
            }
        }
    }

    public IEnumerator MovePrefab()
    {

        yield return new WaitForSeconds(0.1f);
        Ray ray ;
        RaycastHit hit;
        while (isItemClicked)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out hit, Mathf.Infinity))
            {
                //print("힛포인트 " + hit.point);
                cropObject.transform.position = hit.point;

                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.CompareTag("Land") && Input.GetMouseButtonDown(0))
                {
                    Land land = hitObject.GetComponent<Land>();
                    if(land.landStatus == Land.LandStatus.Grass)//tlqkf dhodksehl
                    {
                        isItemClicked = false;
                        Destroy(cropObject);
                    }
                    else if (land.landStatus != Land.LandStatus.Grass && land.cropPlanted == null)
                    {
                        print("들어오니?");
                       // cropObject.transform.position = hit.point; 
                        cropObject.transform.position =  new Vector3(hitObject.transform.position.x, .08f, hitObject.transform.position.z);
                        land.cropPlanted = cropObject.GetComponent<CropBehaviour>();
                        land.cropPlanted.Plant(inventory.slots[index].item);
                        isItemClicked = false;
                    }
                }
                else if(!hit.collider.CompareTag("Land") && Input.GetMouseButtonDown(0))
                {
                    isItemClicked = false;
                    Destroy(cropObject);
                    yield return null;
                }
            }
            yield return null; // 다음 프레임으로 넘어감
        }

    }

    void DrawRayGizmo(Ray ray, float length)
    {
        Gizmos.DrawLine(ray.origin,ray.origin +ray.direction * length);
    }

    void OnDrawGizmos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            DrawRayGizmo(ray, hit.distance);
        }
    }
}
