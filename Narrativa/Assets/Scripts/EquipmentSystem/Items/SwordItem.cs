using UnityEngine;

public class SwordItem : DragObject, IBaseItem
{
    public string Name => "Sword";

    public ItemType ItemType => ItemType.Sword;
}
