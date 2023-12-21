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
        //GetSiblingIndex => 현재 객체의 ㅎ ㅕㅇ제 관계에서 인덱스를 얻기;
        SlotIndex = transform.GetSiblingIndex();
        UpdateSlotUI();
    }

    //Drag & Drop 시스템 세팅

    public abstract void Ondrop(PointerEventData eventData);
    
    public abstract void UpdateSlotUI();
    
    protected virtual void EnableSlotUI(bool enable) => itemIconImage.enabled = enable;
}
