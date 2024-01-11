using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerMove playerMove;

    public GameObject SellUI;
    public GameObject StallUI;

    Land selectedLand=null;
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
        Collider other = hit.collider;

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

        // override로 구성하는게 나았을듯?..ㅡㅡ..
        if (other.CompareTag("NPC"))
        {
            //마우스클리시 UI활성화, 단 NPC와의 거리가 5미만일때만
            float distance = 
                Vector3.Distance(playerMove.gameObject.transform.position,
                                 other.gameObject.transform.position);
            if (Input.GetMouseButtonDown(0) && distance < 5f)
            {
                print(distance);
                SellUI.SetActive(true);
            }
        }

        if (other.CompareTag("Stall"))
        {
            print("ㅎㅇ");
            //마우스클리시 UI활성화, 단 NPC와의 거리가 5미만일때만
            float distance =
                Vector3.Distance(playerMove.gameObject.transform.position,
                                 other.gameObject.transform.position);
            if (Input.GetMouseButtonDown(0) && distance < 5f)
            {
                print(distance);
                StallUI.SetActive(true);
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
