using UnityEngine;

public class DonkeyItem : DragObject, IBaseItem
{
    public string Name => "Donkey";

    public ItemType ItemType => ItemType.Donkey;
}
