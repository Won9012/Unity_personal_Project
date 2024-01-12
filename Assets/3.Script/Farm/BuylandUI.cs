using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuylandUI : MonoBehaviour
{
    public Inventory inventory;
    public GameObject Oner;
    public Material SwichMaterial;

    public GameObject BuyUI_land;

    private void Start()
    {
        BuyUI_land.SetActive(false);
    }
    public void Clicked_BuyLand()
    {
        print(Land.landoner);
        if(inventory.Money >= 3000 && Land.landoner != Land.Landoner.Yes)
        {
            Land.landoner = Land.Landoner.Yes;
            inventory.Money -= 3000;
            inventory.UpdateMoneyText(inventory.Money);

            Renderer renderer = Oner.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = SwichMaterial;
            }
        }
    }
}
