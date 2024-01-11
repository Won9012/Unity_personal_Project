using UnityEngine;
using UnityEngine.UI;

public class TradeNpc : MonoBehaviour
{
    //
    public PlayerMove player;
    public Tools tools;
    public GameObject SellUI;
    public GameObject Body;
    public Inventory inventory;
    //Player�� ���������� => �÷��̾ Ŭ����
    //Player�� BackPack�� �Ű� �ִ� ���¶��
    //������ �Ǹ� GameObj(UI)Ȱ��ȭ ��ų��.
    //�Ǹ� ��ư�� �����ٸ�, �����賶�� �����ϰ�, �賶�� ��� �ڽ�Ʈ �� ��ŭ
    //�κ��丮�� �߰����ְ� ����
    private void Awake()
    {
        SellUI.SetActive(false);
    }

    public void SellBackpack_btn()
    {
        if(player.equipedBackpack == PlayerMove.EquipedBackpack.Equiped)
        {
            Backpack backpack = Body.transform.GetChild(0).gameObject.GetComponent<Backpack>();
            inventory.Money += backpack.BackpackPrice;
            inventory.UpdateMoneyText(inventory.Money);

            Destroy(backpack.gameObject);
            SellUI.SetActive(false);
            player.equipedBackpack = PlayerMove.EquipedBackpack.NotEquiped;
            tools.Sell_or_Car_Backpack();
        }
    }
}
