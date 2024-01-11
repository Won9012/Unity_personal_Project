using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stall : MonoBehaviour
{

    ItemData itemToDisplay;
    public ItemBuffer itemBuffer;
    private List<Slot> slots;

    public Sprite[] Product_Img; //���� ���� �̹��� ǥ�����ٰ�
    public Text Product_text; //Ư��ǰ ���� ��ưŬ���� �̹��� ǥ�� �� �ý�Ʈ ��ȯ

    //�κ��丮�� ������ ��ᷮ �Ǵ�
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

    //Ŭ���ؼ� �����ϴ� �༮ �̸� �����ҳ�
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


    //Stall ���� ������ ������ ������ ��ư��.

    public void CreatBackPack_Con()
    {
        Product_text.text = "[����]������ Ư��ǰ";
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
        Product_text.text = "[����]���� Ư��ǰ";
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
        Product_text.text = "[����]ȣ�� Ư��ǰ";
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
        Product_text.text = "[����]�丶�� Ư��ǰ";
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
        Product_text.text = "[����]���� Ư��ǰ";
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
                Need_IngredientCount = 3; //�ʿ��� ������ ����
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
        //�ߺ����� ������
        isCreatingBackpack = true;
        slider_obj.SetActive(isCreatingBackpack);
        float elapsedTime = 0f;
        slider.value = 0.001f;
        //�����̴� �ٷ� ĳ���� Ÿ�� UI �����ֱ� 
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            //�����̴� value�� ����
            float t = elapsedTime / duration;
            float sliderValue = Mathf.Lerp(0f, 1f, t);
            slider.value = sliderValue;
            yield return null;
        }
            
        slider.value = 1f; // �����̴��� ��Ȯ�ϰ� ������



        //Todo : �������� 
        //ĳ������ ���¸� ���� ���·� ����
        //ĳ������ � ������ ����

        Instantiate(Backpack, Body.transform);
        player.equipedBackpack = PlayerMove.EquipedBackpack.Equiped; //������ �Ű��ִ� ���·� ���� => ������ �Ű��������� ������ �� ���� �ϴ¿뵵
        tools.Get_Backpack();
        isCreatingBackpack = false;
        slider_obj.SetActive(isCreatingBackpack);

        //------------Stall ���� �ý�Ʈ ������Ʈ-------------//
        //����Ѹ�ŭ ������ ����
        pocketSeedCount -= Need_IngredientCount;
        pocketWoodCount -= Need_Ingredient2Count;
        //�ؽ�Ʈ ������Ʈ
        ingredient_Count.text = pocketSeedCount.ToString();
        ingredient2_Count.text = pocketWoodCount.ToString();

        //-------inventory���� update----------//
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            //���� ������ ����
            if(inventory.slots[i].item.name == NowClicked_Item)
            {
                //���� �κ��丮���� ���� ���� �˻��Ǵ� �༮�� ������
                //�ʿ� ��� �������� ������� ���ְ�
                //���ٸ�, 0�̵ɶ����� ������, �������� �ڿ��༮���� ã�Ƽ� ���ָ��
                if(inventory.slots[i].item.count >= Need_IngredientCount)
                {
                    inventory.slots[i].item.count -= Need_IngredientCount;
                    inventory.UpdateSlotText(inventory.slots[i]);
                    //���� �������� 0���� �Ǵ°�쿡�� �κ��丮 ������ ��Ȳ�� ������Ʈ 
                    if(inventory.slots[i].item.count == 0)
                    {
                        inventory.slots[i].item = null;
                        inventory.slots[i].Setitem(inventory.slots[i].item, i); //�κ��丮���� ���� ����(����)
                    }
                }
            }

        }
        for (int i = 0; i < inventory.slots.Count; i++)
        {
            //���� ������ ����
            if (inventory.slots[i].item != null &&inventory.slots[i].item.name == Wood_Item)
            {
                inventory.slots[i].item.count -= Need_Ingredient2Count;
                inventory.UpdateSlotText(inventory.slots[i]);
                //���� �������� 0���� �Ǵ°�쿡�� �κ��丮 ������ ��Ȳ�� ������Ʈ 
                if (inventory.slots[i].item.count == 0 )
                {
                    inventory.slots[i].item = null;
                    inventory.slots[i].Setitem(inventory.slots[i].item, i); //�κ��丮���� ���� ����(����)
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

    
