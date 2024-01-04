using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerMove playerMove;

    Land selectedLand=null;
    void Start()
    {
        playerMove = transform.parent.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
/*        RaycastHit hit;
        if(Physics.Raycast(transform.position,Vector3.down,out hit, 1))
        {
            OninteractableHit(hit);
        }
*/
        // Check if the left mouse button is pressed
/*        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                OnInteractableHit(hit);
            }
        }*/
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
    }

    //��ȣ �ۿ� raycast�� �ε��� �� �߻��ϴ� �� ó�� 
    void OninteractableHit(RaycastHit hit)
    {
        Collider other = hit.collider;
        //print(other);
        if(other.tag == "Land")
        {
            Land land = other.GetComponent<Land>();
            SelectLand(land);
            return;
        }

        if(selectedLand != null)
        {
            selectedLand.Select(false);
            selectedLand = null;
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
