using UnityEngine;

public class FoodItem : DragObject, IBaseItem
{
    public string Name => "Food";
    public ItemType ItemType => ItemType.Food;
}
