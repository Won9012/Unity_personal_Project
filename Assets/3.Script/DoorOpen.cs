using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public PlayerMove playerMove;
    private bool isDoorOpen = false;
    private Quaternion originalRotation;
    private Quaternion targetRotation;

    public float rotationSpeed = 30f;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = Quaternion.Euler(0, 270, 0) * originalRotation;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& Land.landoner == Land.Landoner.Yes)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("FenceDoor") && Land.landoner == Land.Landoner.Yes)
                {
                    float distance = Vector3.Distance(playerMove.gameObject.transform.position, hit.collider.gameObject.transform.position);

                    if (distance < 5f)
                    {
                        if (isDoorOpen)
                        {
                            StartCoroutine(RotateDoor(originalRotation));
                        }
                        else
                        {
                            StartCoroutine(RotateDoor(targetRotation));
                        }
                        isDoorOpen = !isDoorOpen;
                    }
                }
            }
        }
    }

    IEnumerator RotateDoor(Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        Quaternion startingRotation = transform.rotation;

        while (elapsedTime < 3f)
        {
            transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
