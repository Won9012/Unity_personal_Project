using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StallControll : MonoBehaviour
{
    public GameObject StallUI;
    public PlayerMove player;

    private void Start()
    {
        StallUI.SetActive(false);
    }



    private void OnTriggerStay(Collider other)
    {
        print("¤±¤¤¤·");
        if (other.gameObject.CompareTag("Player") && player.equipedBackpack == PlayerMove.EquipedBackpack.NotEquiped)
        {
            StallUI.SetActive(true);
        }
        else
        {
            StallUI.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StallUI.SetActive(false);
    }

}
