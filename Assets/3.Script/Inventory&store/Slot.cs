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


    //�κ��丮���� �������� �������� ������ �޾��ٰ�@
    //�κ��丮���� ���������� �δ콺��ȣ�� �����ͼ� �ϰ�
    //�����Կ��� ������ ���� �ش�������� �ִٸ� ���� ���� �δ콺��ȣ(�κ��丮 ���� �տ��ִ³༮�� �켱���)

    public void OnPointerClick(PointerEventData eventData)
    {
        if (slotType == SlotType.Inventory)
        {
            //�켱 Ŭ�������� �������� ������ ���콺�����Ϳ� ����ְ� , ������ ����
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
                //print("������Ʈ " + hit.point);
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
                        print("������?");
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
            yield return null; // ���� ���������� �Ѿ
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
