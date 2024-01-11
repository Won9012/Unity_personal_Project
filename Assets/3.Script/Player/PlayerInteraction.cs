using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerMove playerMove;

    public GameObject SellUI;
    public GameObject StallUI;

    Land selectedLand=null;
    
    public Texture2D cursorTextureA;
    public Texture2D cursorTextureB;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public GameObject Body;

    public Tools tools;
    
    private bool isBoxHaveItem = false;

    void Start()
    {
        playerMove = transform.parent.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            OnInteractableHit(hit);
        }
    }

    void OnInteractableHit(RaycastHit hit)
    {
        if (SellUI.activeSelf || StallUI.activeSelf) return; //UI가 켜져있는 경우엔 걍 리턴?
        Collider other = hit.collider;
        if (!hit.collider.CompareTag("NPC") && !hit.collider.CompareTag("Stall") && !hit.collider.CompareTag("Box"))
        {
            Cursor.SetCursor(cursorTextureA, hotSpot, cursorMode);
        }
        if (other.CompareTag("Land"))
        {
            Land land = other.GetComponent<Land>();
            SelectLand(land);
            return;
        }

        if (selectedLand != null)
        {
            selectedLand.Select(false);
            selectedLand = null;
        }

        if (other.CompareTag("Box"))
        {
            Cursor.SetCursor(cursorTextureB, hotSpot, cursorMode);
            float distance = Vector3.Distance(playerMove.gameObject.transform.position,
                                               other.gameObject.transform.position);
            print(other.gameObject.name);
            if (Input.GetMouseButtonDown(1) && distance <6.5f && Tools.toolType == Tools.ToolType.EquipedBackpack && !isBoxHaveItem)
            {
                Backpack backpack = Body.transform.GetChild(0).gameObject.GetComponent<Backpack>();
                isBoxHaveItem = true;
                print(other.gameObject.name);
                backpack.gameObject.transform.SetParent(other.gameObject.transform.GetChild(0));
                backpack.gameObject.transform.position = other.gameObject.transform.GetChild(0).transform.position;
                backpack.gameObject.transform.rotation = other.gameObject.transform.GetChild(0).transform.rotation;
                Tools.toolType = Tools.ToolType.Empty;
                playerMove.equipedBackpack = PlayerMove.EquipedBackpack.NotEquiped;
                tools.Sell_or_Car_Backpack();
            }
            else
            {
                if (Input.GetMouseButtonDown(1) && distance < 6.5f && Tools.toolType == Tools.ToolType.Empty && isBoxHaveItem)
                {
                    GameObject Backpack = other.gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                    Backpack.transform.SetParent(null);
                    Backpack.transform.SetParent(Body.transform);
                    Backpack.transform.position = Body.transform.position;
                    Backpack.transform.rotation = Body.transform.rotation;
                    Tools.toolType = Tools.ToolType.EquipedBackpack;
                    playerMove.equipedBackpack = PlayerMove.EquipedBackpack.Equiped;
                    isBoxHaveItem = false;
                }
            }



        }

        // override로 구성하는게 나았을듯?..ㅡㅡ..
        if (other.CompareTag("NPC"))
        {
            Cursor.SetCursor(cursorTextureB, hotSpot, cursorMode);
            float distance = 
                Vector3.Distance(playerMove.gameObject.transform.position,
                                 other.gameObject.transform.position);
            if (Input.GetMouseButtonDown(0) && distance < 5f)
            {
                SellUI.SetActive(true);
                Cursor.SetCursor(cursorTextureA, hotSpot, cursorMode);
            }
        }

        if (other.CompareTag("Stall"))
        {
            Cursor.SetCursor(cursorTextureB, hotSpot, cursorMode);
            //마우스클리시 UI활성화, 단 NPC와의 거리가 5미만일때만
            float distance =
                Vector3.Distance(playerMove.gameObject.transform.position,
                                 other.gameObject.transform.position);
            if (Input.GetMouseButtonDown(0) && distance < 5f)
            {
                StallUI.SetActive(true);
                Cursor.SetCursor(cursorTextureA, hotSpot, cursorMode);
            }
        }
    }
    void SelectLand(Land land)
    {
        if (selectedLand != null)
        {
            selectedLand.Select(false);
        }

        //set new selected land to the land selecting now 
        //새로운땅을 선택하는 것으로 셋팅
        selectedLand = land;
        land.Select(true);
    }

    //플레이어가 버튼을 눌렀을때 상호작용할키
    public void Interact(Tools.ToolType toolType)
    {
        if(selectedLand != null)
        {
            selectedLand.Interact(toolType);
            return;
        }
        print("Not land!");
    }
}
