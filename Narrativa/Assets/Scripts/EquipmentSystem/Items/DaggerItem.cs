using UnityEngine;

public class DaggerItem : DragObject, IBaseItem
{
    public string Name => "Dagger";

    public ItemType ItemType => ItemType.Dagger;
}
