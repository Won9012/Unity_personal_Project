using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Car : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4];
    public Transform[] tires = new Transform[4];
    public float maxF = 200f;
    private float minF = 0f;
    public float power = 6000.0f; 
    public float rot = 45;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    void Awake()
    {
        TryGetComponent(out rb);
        for (int i = 0; i < 4; i++)
        {
            wheels[i].steerAngle = 0; 
            wheels[i].ConfigureVehicleSubsteps(5, 12, 13);
        }
        rb.centerOfMass = new Vector3(0, 0, 0);
    }
    private void Update()
    {
      //  UpdateMeshesPostion();
    }



    void FixedUpdate()
    {
       // moveCar();
        MoveCar();
        StopCar();
    }

    private void moveCar()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection = (v * Vector3.forward + h * Vector3.right).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        for (int i = 0; i < 4; i++)
        {
            wheels[i].motorTorque = maxF * v;
        }
        float steer = rot * h;
        for (int i = 0; i < 4; i++)
        {
            wheels[i].steerAngle = steer;
        }
    }


    private void MoveCar()
    {


        float a = Input.GetAxis("Vertical");
        float steer = rot * Input.GetAxis("Horizontal");
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].motorTorque = maxF * a; //바퀴를 돌린다.
        }
        for (int i = 0; i < 2; i++)
        {
            //앞바퀴 두개만 회전
            wheels[i].steerAngle = steer;
        }
    }

    private void StopCar()
    {
        if (!Input.anyKey)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = 2000f;
            }
        }
        else
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = 0f;
            }
        }
    }
    void UpdateMeshesPostion()
    {
        for (int i = 0; i < 2; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheels[i].GetWorldPose(out pos, out quat);
            tires[i].position = pos;
            tires[i].rotation = quat;
        }
    }
}