using UnityEngine;

public class FoodItem : DragObject, IBaseItem
{
    public string Name => "Food Item";
    public ItemType ItemType => ItemType.Food;
}
