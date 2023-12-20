using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FreeLookControll : MonoBehaviour
{
    public float scrollSpeed = 5f;
    void Awake()
    {
        CinemachineCore.GetInputAxis = clickControl;
    }

    public float clickControl(string axis)
    {
        if (Input.GetMouseButton(1))
        {
            return UnityEngine.Input.GetAxis(axis);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

            Vector3 cameraDirection = transform.localRotation * Vector3.forward;

            transform.position += cameraDirection * Time.deltaTime * scrollWheel * scrollSpeed;
            return 0;
        }

        return 0;
    }
}
