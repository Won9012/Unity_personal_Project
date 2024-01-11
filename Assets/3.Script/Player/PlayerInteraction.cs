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

    public Tools tools;
    
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
        if (SellUI.activeSelf || StallUI.activeSelf) return; //UI�� �����ִ� ��쿣 �� ����?
        Collider other = hit.collider;
        if (!hit.collider.CompareTag("NPC") && !hit.collider.CompareTag("Stall"))
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

        // override�� �����ϴ°� ��������?..�Ѥ�..
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
            //���콺Ŭ���� UIȰ��ȭ, �� NPC���� �Ÿ��� 5�̸��϶���
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
        //���ο�� �����ϴ� ������ ����
        selectedLand = land;
        land.Select(true);
    }

    //�÷��̾ ��ư�� �������� ��ȣ�ۿ���Ű
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
