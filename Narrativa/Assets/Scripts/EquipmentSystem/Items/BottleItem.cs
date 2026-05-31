using UnityEngine;

public class BottleItem : DragObject, IBaseItem
{
    public string Name => "Bottle";

    public ItemType ItemType => ItemType.Bottle;
}
