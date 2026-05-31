using UnityEngine;

public class BagItem : DragObject, IBaseItem
{
    public string Name => "Bag";

    public ItemType ItemType => ItemType.Bag;
}
