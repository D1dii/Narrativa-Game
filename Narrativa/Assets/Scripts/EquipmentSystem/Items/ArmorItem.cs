using UnityEngine;

public class ArmorItem : DragObject, IBaseItem
{
    public string Name => "Armor";
    public ItemType ItemType => ItemType.Armor;

}
