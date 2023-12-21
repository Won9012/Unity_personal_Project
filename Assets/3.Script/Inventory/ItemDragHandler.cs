using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUphandler, IpointerEnterHandler, IpointerExitHandler
{
    [SerializeField] protected ItemSlotUI itemSlotUI = null;

    private CanvasGroup canvasGroup = null;
    private Transform originalParent = null;
    private bool isHovering = false;

    public ItemSlotUI ItemSlotUI => itemSlotUI;

    private void Start() => TryGetComponent(out canvasGroup);

    private void OnDisable()
    {
        if (isHovering)
        {
            //rasie event
            isHovering = false;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //raise event

            originalParent = transform.parent;

            transform.SetParent(transform.parent.parent);

            canvasGroup.blocksRaycasts = false;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Alert any listeners that we have started hovering
        //raise event
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //raise event
        isHovering = false;
    }


}
