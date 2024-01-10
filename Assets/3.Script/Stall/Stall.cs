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

    private bool isCreatingBackpack = false;

    private void Awake()
    {
        Product_text.text = "";
        Need_IngredientCount = 0;
        Need_Ingredient2Count = 1;
        ingredient_Count.text = "";
        ingredient2_Count.text = "";
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
        }
    }

    public void Confirm_Backpack()
    {
        StartCoroutine(Creat_BackPack());
        if (isEnough && !isCreatingBackpack && player.equipedBackpack == PlayerMove.EquipedBackpack.NotEquiped)
        {
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


        if (pocketSeedCount > Need_IngredientCount && pocketWoodCount > Need_Ingredient2Count)
        {
            isEnough = true;
        }
        else
        {
            isEnough = false;
        }

        //Todo : 등짐제작 
        //캐릭터의 상태를 백팩 상태로 변경
        //캐릭터의 등에 등짐을 생성

        Instantiate(Backpack, Body.transform);
        player.equipedBackpack = PlayerMove.EquipedBackpack.Equiped; //등짐을 매고있는 상태로 변경 => 등짐을 매고있을때는 제작할 수 없게 하는용도
        isCreatingBackpack = false;
        slider_obj.SetActive(isCreatingBackpack);

    }
}

    
