using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    //타기버튼 (타기전상황)
    [SerializeField] private GameObject RideOn_btn;
    [SerializeField] private GameObject RideOff_btn;
    [SerializeField] private PlayerMove player_m;
    [SerializeField] private CarMove carMove;
    [SerializeField] private Rigidbody player_rb;
    [SerializeField] private Animator player_anim;
    [SerializeField] private Rigidbody car_rb;
    [SerializeField] private GameObject middle;

    [SerializeField] private GameObject player;

    //타고 난 이후에 출력해줄 UI (타고있는상황)
    [SerializeField] private GameObject Riding_UI;
    [SerializeField] private GameObject ImageRideOn;
    [SerializeField] private GameObject ImageRideOff;

    [SerializeField] private GameObject car;
    [SerializeField] private Transform carOffposition;


    static public bool isRide = false;

    private void Start()
    {
        ImageRideOff.SetActive(false);
    }

    public void Click_RideOn_btn()
    {
        isRide = true;
        player_m.transform.position = new Vector3(middle.transform.position.x, middle.transform.position.y +0.3f, middle.transform.position.z);
        player_m.transform.rotation = car.transform.rotation;
        player.transform.parent = middle.transform;
        player_rb.useGravity = false;
        player_rb.isKinematic = true;
        car_rb.useGravity = true;
        car_rb.isKinematic = false;
        player_m.enabled = false;
        carMove.enabled = true;
        ImageRideOn.SetActive(false);
        ImageRideOff.SetActive(true);
        player_anim.SetBool("isWalk", false);

    }

    public void Click_RideOff_btn()
    {
        isRide = false;
        player_m.transform.position = carOffposition.position;
        player_m.enabled = true;
        player.transform.parent = null;
        player_rb.useGravity = true;
        player_rb.isKinematic = false;
        car_rb.useGravity = false;
        car_rb.isKinematic = true;
        carMove.enabled = false;
        ImageRideOff.SetActive(false);
    }

}
