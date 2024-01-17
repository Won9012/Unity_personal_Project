using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private float Walk_Speed = 5f;
    [SerializeField] private float EquiptedBackpackWalk_Speed = 2f;
    [SerializeField] private float RotationSpeed = 3f;

    [Header("Camera")]
     private Camera camera;

    //Animator 동작 관련 bool 변수 설정
    private bool iswalk = false;


    //Interaction components
    PlayerInteraction playerInteraction;

    public  enum EquipedBackpack
    {
        NotEquiped, Equiped
    }

    public EquipedBackpack equipedBackpack;

    private void Awake()
    {
        TryGetComponent(out anim);
        camera = Camera.main;
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
        DataManager.instance.LoadData();
        transform.position = DataManager.instance.nowPlayer.position;
    }

    private void Update()
    {
        Player_Move();
        Interact(Tools.toolType);
        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            TimeManager.Instance.Tick();
        }
        DataManager.instance.nowPlayer.position = transform.position;
        DataManager.instance.SaveData();
    }

    public void Interact(Tools.ToolType toolType)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //interact
            playerInteraction.Interact(toolType);
        }

        //Todo: Set up item interaction;
    }

    private void Player_Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        Vector3 moveDirection = (v * cameraForward + h * cameraRight).normalized;

        if (Mathf.Abs(v) > 0 || Mathf.Abs(h) > 0)
        {
            iswalk = true;
            anim.SetBool("isWalk", iswalk);
        }
        else
        {
            iswalk = false;
            anim.SetBool("isWalk", iswalk);
        }

        //플레이어가 이동 방향을 바라보도록 회전
        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(moveDirection, Vector3.up),
                RotationSpeed * Time.deltaTime
            );
        }

        //백팩을 끼고 있다면 이동속도 느리게 => 백팩은 아이템 조합해서 무역상품
        if(Tools.toolType != Tools.ToolType.EquipedBackpack)
        {
            transform.position += moveDirection * Walk_Speed * Time.deltaTime;
        }
        else if(Tools.toolType == Tools.ToolType.EquipedBackpack)
        {
            transform.position += moveDirection * EquiptedBackpackWalk_Speed * Time.deltaTime;
        }
       
    }

}
