using System.Text;
using UnityEngine;

public class ConsumableItem : InventoryItem
{
    [Header("Consumable Data")]
    [SerializeField] private string useTesxt = "Does something, maybe?";
    public override string GetInfoDisPlayText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(Name).AppendLine();
        builder.Append("<color = green>Use: ").Append(useTesxt).Append("</color>").AppendLine();
        builder.Append("Max Stack: ").Append(MaxStack).AppendLine();
        builder.Append("Sell Price: ").Append(SellPrice).Append(" Gold");
        return builder.ToString();
    }
}
