using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDragHandler : ItemDragHandler
{
    public override void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            base.OnPointerUp(eventData);
            //������ ���� �Ҳ���? �ϰ� ���� â ����.
            if(eventData.hovered.Count == 0)
            {
                //destroy item or drop item
            }

        }
    }
}
