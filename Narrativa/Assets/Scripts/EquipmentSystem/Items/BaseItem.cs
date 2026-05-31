using UnityEngine;

public enum ItemType
{
    Food,
    Sword,
    Armor,
    Bag,
    Bow,
    Bottle,
    Dagger,
    Donkey

}

public interface IBaseItem
{
    string Name { get; }
    ItemType ItemType { get; }
}
