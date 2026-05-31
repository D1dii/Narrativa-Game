using UnityEngine;

public class BowItem : DragObject, IBaseItem
{
    public string Name => "Bow";

    public ItemType ItemType => ItemType.Bow;
}
