using UnityEngine;

public abstract class InventoryItem : HotbarItem
{
    [Header("Item Data")]
    [Min(0)]private int sellPrice = 1;
    [Min(0)]private int maxStack = 1;

    public override string ColouredName
    {
        get
        {
            return Name;
        }
    }
   // public int SellPrice {  get { return sellPrice; } }
    public int SellPrice => sellPrice;
    //public int MaxStack {  get { return maxStack; } }
    public int MaxStack => maxStack;

}
