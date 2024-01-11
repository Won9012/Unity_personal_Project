using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stall : MonoBehaviour
{

    ItemData itemToDisplay;
    public ItemBuffer itemBuffer;
    private List<Slot> slots;

    public Sprite[] Product_Img; //등짐 옆에 이미지 표기해줄거
    public Text Product_text; //특산품 설명 버튼클릭시 이미지 표기 및 택스트 전환

    //인벤토리에 보유한 재료량 판단
    public Text ingredient_Count;
    public int Need_IngredientCount = 0;
    public Text ingredient2_Count;
    public int Need_Ingredient2Count=1;
    public Inventory inventory;

    public Image ingredient_Img;
    public Image ingredient2_Img;

    public static bool isEnough = false;

    float pocketSeedCount = 0;
    float pocketWoodCount = 0;

    public float duration = 3f;
    public Slider slider;
    public GameObject slider_obj;

    public GameObject Backpack;
    public GameObject Body;

    public PlayerMove player;

    public Tools tools;

    private bool isCreatingBackpack = false;

    public GameObject StallUI;

    //클릭해서 생산하는 녀석 이름 저장할놈
    private string NowClicked_Item;
    private string Wood_Item;

    private void Awake()
    {
        Product_text.text = "";
        Need_IngredientCount = 0;
        Need_Ingredient2Count = 1;
        ingredient_Count.text = "";
        ingredient2_Count.text = "";
        NowClicked_Item = "";
        slider_obj.SetActive(false);
    }

    private void Start()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < slots.Count; i++)
            {
                print(slots[i].item.Description);
            }
        }
    }


    //Stall 에서 누르면 아이템 제작할 버튼들.

    public void CreatBackPack_Con()
    {
        Product_text.text = "[등짐]옥수수 특산품";
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if(inventory.slots[i].item.name == "Corn_Harvestabe")
            {
                ingredient_Img.sprite = inventory.slots[i].item.sprite;
                Need_IngredientCount = 3;
                pocketSeedCount += inventory.slots[i].item.count;
                ingredient_Count.text = $"{pocketSeedCount} / {Need_IngredientCount}";
                if(pocketSeedCount < Need_IngredientCount)
                {
                    isEnough = false;
                    ingredient_Count.color = Color.red;
                }
                else
                {
                    isEnough = true;
                    ingredient_Count.color = Color.black;
                }
                NowClicked_Item = inventory.slots[i].item.name;
            }
        }

        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item.name == "Tree_Harvestabe")
            {
                ingredient2_Img.sprite = inventory.slots[i].item.sprite;
                pocketWoodCount += inventory.slots[i].item.count;
                ingredient2_Count.text = $"{pocketWoodCount} / {Need_Ingredient2Count}";
                if (pocketWoodCount < Need_Ingredient2Count)
                {
                    isEnough = false;
                    ingredient2_Count.color = Color.red;
                }
                else
                {
                    isEnough = true;
                    ingredient2_Count.color = Color.black;
                }
                Wood_Item = inventory.slots[i].item.name;
            }
        }
    }
    public void CreatBackPack_Egg()
    {
        Product_text.text = "[등짐]가지 특산품";
        ingredient_Count.text = "";
        ingredient2_Count.text = "";
        pocketSeedCount = 0;
        pocketWoodCount = 0;
        ingredient_Img.sprite = null;
        ingredient2_Img.sprite = null;
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item.name == "Eggplant_Harvestabe")
            {
                ingredient_Img.sprite = inventory.slots[i].item.sprite;
                Need_IngredientCount = 3;
                pocketSeedCount += inventory.slots[i].item.count;
                ingredient_Count.text = $"{pocketSeedCount} / {Need_IngredientCount}";
                if (pocketSeedCount < Need_IngredientCount)
                {
                    isEnough = false;
                    ingredient_Count.color = Color.red;
                }
                else
                {
                    isEnough = true;
                    ingredient_Count.color = Color.black;
                }
             NowClicked_Item = inventory.slots[i].item.name;
            }
        }

        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item.name == "Tree_Harvestabe")
            {
                ingredient2_Img.sprite = inventory.slots[i].item.sprite;
                pocketWoodCount += inventory.slots[i].item.count;
                ingredient2_Count.text = $"{pocketWoodCount} / {Need_Ingredient2Count}";
                if (pocketWoodCount < Need_Ingredient2Count)
                {
                    isEnough = false;
                    ingredient2_Count.color = Color.red;
                }
                else
                {
                    isEnough = true;
                    ingredient2_Count.color = Color.black;
                }
                Wood_Item = inventory.slots[i].item.name;
            }
        }
    }
    public void CreatBackPack_Pumpkin()
    {
        Product_text.text = "[등짐]호박 특산품";
        ingredient_Count.text = "";
        ingredient2_Count.text = "";
        pocketSeedCount = 0;
        pocketWoodCount = 0;
        ingredient_Img.sprite = null;
        ingredient2_Img.sprite = null;
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item.name == "Pumpkin_Harvestabe")
            {
                ingredient_Img.sprite = inventory.slots[i].item.sprite;
                Need_IngredientCount = 3;
                pocketSeedCount += inventory.slots[i].item.count;
                ingredient_Count.text = $"{pocketSeedCount} / {Need_IngredientCount}";
                if (pocketSeedCount < Need_IngredientCount)
                {
                    isEnough = false;
                    ingredient_Count.color = Color.red;
                }
                else
                {
                    isEnough = true;
                    ingredient_Count.color = Color.black;
                }
                NowClicked_Item = inventory.slots[i].item.name;
            }
        }

        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item.name == "Tree_Harvestabe")
            {
                ingredient2_Img.sprite = inventory.slots[i].item.sprite;
                pocketWoodCount += inventory.slots[i].item.count;
                ingredient2_Count.text = $"{pocketWoodCount} / {Need_Ingredient2Count}";
                if (pocketWoodCount < Need_Ingredient2Count)
                {
                    isEnough = false;
                    ingredient2_Count.color = Color.red;
                }
                else
                {
                    isEnough = true;
                    ingredient2_Count.color = Color.black;
                }
            }
            Wood_Item = inventory.slots[i].item.name;
        }
    }
    public void CreatBackPack_Tomato()
    {
        Product_text.text = "[등짐]토마토 특산품";
        ingredient_Count.text = "";
        ingredient2_Count.text = "";
        pocketSeedCount = 0;
        pocketWoodCount = 0;
        ingredient_Img.sprite = null;
        ingredient2_Img.sprite = null;
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item.name == "Tomato_Harvestabe")
            {
                ingredient_Img.sprite = inventory.slots[i].item.sprite;
                Need_IngredientCount = 3;
                pocketSeedCount += inventory.slots[i].item.count;
                ingredient_Count.text = $"{pocketSeedCount} / {Need_IngredientCount}";
                if (pocketSeedCount < Need_IngredientCount)
                {
                    isEnough = false;
                    ingredient_Count.color = Color.red;
                }
                else
                {
                    isEnough = true;
                    ingredient_Count.color = Color.black;
                }
                NowClicked_Item = inventory.slots[i].item.name;                
            }
            
        }

        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item.name == "Tree_Harvestabe")
            {
                ingredient2_Img.sprite = inventory.slots[i].item.sprite;
                pocketWoodCount += inventory.slots[i].item.count;
                ingredient2_Count.text = $"{pocketWoodCount} / {Need_Ingredient2Count}";
                if (pocketWoodCount < Need_Ingredient2Count)
                {
                    isEnough = false;
                    ingredient2_Count.color = Color.red;
                }
                else
                {
                    isEnough = true;
                    ingredient2_Count.color = Color.black;
                }
                Wood_Item = inventory.slots[i].item.name;
            }
        }
    }
    public void CreatBackPack_Turnip()
    {
        Product_text.text = "[등짐]순무 특산품";
        ingredient_Count.text = "";
        ingredient2_Count.text = "";
        pocketSeedCount = 0;
        pocketWoodCount = 0;
        ingredient_Img.sprite = null;
        ingredient2_Img.sprite = null;

        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item.name == "Turnip_Harvestabe")
            {
                ingredient_Img.sprite = inventory.slots[i].item.sprite;
                Need_IngredientCount = 3; //필요한 아이템 갯수
                pocketSeedCount += inventory.slots[i].item.count;
                ingredient_Count.text = $"{pocketSeedCount} / {Need_IngredientCount}";
                if (pocketSeedCount < Need_IngredientCount)
                {
                    isEnough = false;
                    ingredient_Count.color = Color.red;
                }
                else
                {
                    isEnough = true;
                    ingredient_Count.color = Color.black;
                }
                NowClicked_Item = inventory.slots[i].item.name;
            }
        }
        
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            if (inventory.slots[i].item.name == "Tree_Harvestabe")
            {
                ingredient2_Img.sprite = inventory.slots[i].item.sprite;
                pocketWoodCount += inventory.slots[i].item.count;
                ingredient2_Count.text = $"{pocketWoodCount} / {Need_Ingredient2Count}";
                if (pocketWoodCount < Need_Ingredient2Count)
                {
                    isEnough = false;
                    ingredient2_Count.color = Color.red;
                }
                else
                {
                    isEnough = true;
                    ingredient2_Count.color = Color.black;
                }
                Wood_Item = inventory.slots[i].item.name;
            }
        }
    }

    public void Confirm_Backpack()
    {
        if (isEnough && !isCreatingBackpack && player.equipedBackpack == PlayerMove.EquipedBackpack.NotEquiped)
        {
            StartCoroutine(Creat_BackPack());
        }
    }

    public void StopAllCourutine_Stall()
    {
        StopAllCoroutines();
        slider_obj.SetActive(false);
        isCreatingBackpack = false;
        isEnough = true;
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.gameObject.transform.position,gameObject.transform.position);

        if(distance > 3f && StallUI.activeSelf)
        {
            StopAllCoroutines();
            slider_obj.SetActive(false);
            isCreatingBackpack = false;
            isEnough = true;
            StallUI.SetActive(false);
        }
    }
    private IEnumerator Creat_BackPack()
    {
        isEnough = false;
        //중복실행 방지용
        isCreatingBackpack = true;
        slider_obj.SetActive(isCreatingBackpack);
        float elapsedTime = 0f;
        slider.value = 0.001f;
        //슬라이더 바로 캐스팅 타임 UI 보여주기 
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            //슬라이더 value값 조정
            float t = elapsedTime / duration;
            float sliderValue = Mathf.Lerp(0f, 1f, t);
            slider.value = sliderValue;
            yield return null;
        }
            
        slider.value = 1f; // 슬라이더값 명확하게 재지정



        //Todo : 등짐제작 
        //캐릭터의 상태를 백팩 상태로 변경
        //캐릭터의 등에 등짐을 생성

        Instantiate(Backpack, Body.transform);
        player.equipedBackpack = PlayerMove.EquipedBackpack.Equiped; //등짐을 매고있는 상태로 변경 => 등짐을 매고있을때는 제작할 수 없게 하는용도
        tools.Get_Backpack();
        isCreatingBackpack = false;
        slider_obj.SetActive(isCreatingBackpack);

        //------------Stall 에서 택스트 업데이트-------------//
        //사용한만큼 아이템 감소
        pocketSeedCount -= Need_IngredientCount;
        pocketWoodCount -= Need_Ingredient2Count;
        //텍스트 업데이트
        ingredient_Count.text = pocketSeedCount.ToString();
        ingredient2_Count.text = pocketWoodCount.ToString();

        //-------inventory에서 update----------//
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            //씨앗 아이템 갱신
            if(inventory.slots[i].item.name == NowClicked_Item)
            {
                //현재 인벤토리에서 가장 먼저 검색되는 녀석의 갯수가
                //필요 재료 갯수보다 많을경우 빼주고
                //적다면, 0이될때까지 빼준후, 나머지는 뒤에녀석에서 찾아서 빼주면됨
                if(inventory.slots[i].item.count >= Need_IngredientCount)
                {
                    inventory.slots[i].item.count -= Need_IngredientCount;
                    inventory.UpdateSlotText(inventory.slots[i]);
                    //만약 아이템이 0개가 되는경우에는 인벤토리 아이템 현황도 업데이트 
                    if(inventory.slots[i].item.count == 0)
                    {
                        inventory.slots[i].item = null;
                        inventory.slots[i].Setitem(inventory.slots[i].item, i); //인벤토리에서 정보 갱신(삭제)
                    }
                }
            }

        }
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            //목재 아이템 갱신
            if (inventory.slots[i].item != null &&inventory.slots[i].item.name == Wood_Item)
            {
                inventory.slots[i].item.count -= Need_Ingredient2Count;
                inventory.UpdateSlotText(inventory.slots[i]);
                //만약 아이템이 0개가 되는경우에는 인벤토리 아이템 현황도 업데이트 
                if (inventory.slots[i].item.count == 0 )
                {
                    inventory.slots[i].item = null;
                    inventory.slots[i].Setitem(inventory.slots[i].item, i); //인벤토리에서 정보 갱신(삭제)
                }
            }
        }


        if (pocketSeedCount > Need_IngredientCount && pocketWoodCount > Need_Ingredient2Count)
        {
            isEnough = true;
        }
        else
        {
            isEnough = false;
        }
    }

}

    
