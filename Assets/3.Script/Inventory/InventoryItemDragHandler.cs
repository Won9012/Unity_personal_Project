using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDragHandler : ItemDragHandler
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            base.OnPointerUp(eventData);
            //아이템 삭제 할꺼니? 하고 뭍는 창 구현.
            if(eventData.hovered.Count == 0)
            {
                //destroy item or drop item
            }

        }
    }
}
