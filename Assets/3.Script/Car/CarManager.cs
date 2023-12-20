using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    //타기버튼 (타기전상황)
    [SerializeField] private GameObject Ride_btn;
    [SerializeField] private PlayerMove player_m;
    [SerializeField] private CarMove carMove;
    [SerializeField] private Rigidbody player_rb;
    [SerializeField] private GameObject middle;

    [SerializeField] private GameObject player;

    //타고 난 이후에 출력해줄 UI (타고있는상황)
    [SerializeField] private GameObject Riding_UI;

    static public bool isRide = false;

    //
    private void Update()
    {
        if (isRide)
        {
            Riding_UI.SetActive(true);
        }
        if (!isRide)
        {
            Riding_UI.SetActive(false);
        }
    }
    public void Click_RideOn_btn()
    {
        isRide = true;
        player_m.transform.position = new Vector3(middle.transform.position.x, middle.transform.position.y +0.3f, middle.transform.position.z);
        player_m.transform.rotation = Quaternion.Euler(0, 90f, 0f);
        player.transform.parent = middle.transform;
        player_rb.useGravity = false;
        player_rb.isKinematic = true;
        player_m.enabled = false;
        carMove.enabled = true;
        Ride_btn.SetActive(false);
    }

    public void Click_RideOff_btn()
    {
        isRide = false;
        player_m.transform.position = new Vector3(middle.transform.position.x, middle.transform.position.y, middle.transform.position.z -2f);
        player_m.enabled = true;
        player.transform.parent = null;
        player_rb.useGravity = true;
        player_rb.isKinematic = false;
        carMove.enabled = false;
        Ride_btn.SetActive(false);
    }


}
