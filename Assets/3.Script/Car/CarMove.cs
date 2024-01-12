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


/*    void Update()
    {
        MoveCar();
        StopCar();
        Rotate_WheelPrefab();
    }*/

    private void FixedUpdate()
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
           //aternion newRotation = Quaternion.Euler(quat.eulerAngles.z, quat.eulerAngles.y + 90f, quat.eulerAngles.x);
            Quaternion newRotation = quat * Quaternion.Euler(0f, 90f, 0f);
            tires[i].rotation = newRotation;
            tires[i].position = pos;
        }
    }

    private void StopCar()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = 20000f;
            }
            print(wheels[0].brakeTorque);
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