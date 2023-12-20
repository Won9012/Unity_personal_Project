using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarMove : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4];
    public Transform[] tires = new Transform[4];
    public float maxF = 200f;
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
        
    }


    void Update()
    {
        MoveCar();
        StopCar();
        Rotate_WheelPrefab();
    }
    private void MoveCar()
    {
        float a = Input.GetAxis("Vertical");
        float steer = rot * Input.GetAxis("Horizontal");
        for (int i = 0; i < wheels.Length; i++)
        {
            wheels[i].motorTorque = maxF * a; 
        }
        for (int i = 0; i < 2; i++)
        {
            //앞바퀴 두개만 회전
            wheels[i].steerAngle = steer;
        }
    }
     
   private void Rotate_WheelPrefab()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 pos;
            Quaternion quat;
            wheels[i].GetWorldPose(out pos, out quat);
            Quaternion newRotation = Quaternion.Euler(quat.eulerAngles.z, quat.eulerAngles.y + 90f, quat.eulerAngles.x);

            tires[i].position = pos;
            tires[i].rotation = newRotation;
        }
    }

    private void StopCar()
    {
        if (!Input.anyKey)
        {
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = 20000f;
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
}