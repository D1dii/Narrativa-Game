using UnityEngine;

public enum ItemType
{
    Food,
    Weapon,
    Armor,
}

public interface IBaseItem
{
    string Name { get; }
    ItemType ItemType { get; }
}
