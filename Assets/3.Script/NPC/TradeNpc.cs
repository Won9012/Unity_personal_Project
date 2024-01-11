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
    //Player랑 접촉했을때 => 플레이어가 클릭시
    //Player가 BackPack을 매고 있는 상태라면
    //아이템 판매 GameObj(UI)활성화 시킬것.
    //판매 버튼을 누른다면, 무역배낭을 삭제하고, 배낭에 담긴 코스트 값 만큼
    //인벤토리에 추가해주고 갱신
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
