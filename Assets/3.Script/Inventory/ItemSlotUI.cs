using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public abstract class ItemSlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] protected Image itemIconImage = null;

    public int SlotIndex { get; private set; }

    public abstract HotbarItem SlotItem { get; set; }

    private void OnEnable() => UpdateSlotUI();
    

    protected virtual void Start()
    {
        //GetSiblingIndex => ���� ��ü�� �� �Ť��� ���迡�� �ε����� ���;
        SlotIndex = transform.GetSiblingIndex();
        UpdateSlotUI();
    }

    //Drag & Drop �ý��� ����

    public abstract void Ondrop(PointerEventData eventData);
    
    public abstract void UpdateSlotUI();
    
    protected virtual void EnableSlotUI(bool enable) => itemIconImage.enabled = enable;
}
