using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCar : MonoBehaviour
{
    public Transform CarSommon;
    public Transform Player;
    public GameObject Car;
    public Rigidbody Car_rb;
    private bool iscarOn = false;

    public void Toggle_Car()
    {
        if (CarManager.isRide) return;
        print(iscarOn);
        iscarOn = !iscarOn;
        Car.SetActive(iscarOn);
        Vector3 offset = new Vector3(0, 2f, 0);
        Car.transform.position = CarSommon.transform.position + offset;
        Car.transform.rotation = Player.transform.rotation * Quaternion.Euler(0, 90f, 0);
        if (iscarOn) StartCoroutine(CarRb_co());
    }

    IEnumerator CarRb_co()
    {
        Car_rb.isKinematic = false;
        Car_rb.useGravity = true;
        yield return new WaitForSeconds(1f);
        Car_rb.isKinematic = true;
        Car_rb.useGravity = false;
    }
}
