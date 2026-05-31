using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;


public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;
    public List<IBaseItem> inventory = new List<IBaseItem>();

    public int playerCoins = 0;
    public int foodAmount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void AddItem(IBaseItem item)
    {
        if (item.ItemType == ItemType.Food)
        {
            foodAmount++;
            return;
        }
        inventory.Add(item);
    }

    public void RemoveItem(IBaseItem item)
    {
        inventory.Remove(item);
    }

    [YarnCommand("AddItem")]
    public static void AddItem(string itemName)
    {
        
        switch (itemName)
        {
            case "Sword":
                var sword = new SwordItem();
                Instance.AddItem(sword);
                break;
            case "Armor":
                var armor = new ArmorItem();
                Instance.AddItem(armor);
                break;
            case "Bag":
                var bag = new BagItem();
                Instance.AddItem(bag);
                break;
            case "Bow":
                var bow = new BowItem();
                Instance.AddItem(bow);
                break;
            case "Bottle":
                var bottle = new BottleItem();
                Instance.AddItem(bottle);
                break;
            case "Dagger":
                var dagger = new DaggerItem();
                Instance.AddItem(dagger);
                break;
            case "Donkey":
                var donkey = new DonkeyItem();
                Instance.AddItem(donkey);
                break;
        }
    }

    [YarnCommand("RemoveItem")]
    public static void RemoveItem(string itemName)
    {
        foreach (var item in Instance.inventory)
        {
            if (item.Name == itemName)
            {
                Instance.RemoveItem(item);
                break;
            }
        }
    }

    [YarnFunction("GetFoodAmount")]
    public static int GetFoodAmount()
    {
        return Instance.foodAmount;
    }

    [YarnFunction("GetPlayerCoins")]
    public static int GetPlayerCoins()
    {
        return Instance.playerCoins;
    }

    [YarnCommand("AddCoins")]
    public static void AddCoins(int amount)
    {
        Instance.playerCoins += amount;
    }

    [YarnCommand("RemoveCoins")]
    public static void RemoveCoins(int amount)
    {
        Instance.playerCoins -= amount;
    }

    [YarnCommand("UseFood")]
    public static void UseFood()
    {
        if (Instance.foodAmount > 0)
        {
            Instance.foodAmount--;
        }
    }

    [YarnCommand("AddFood")]
    public static void AddFood(int amount)
    {
        Instance.foodAmount += amount;
    }

    [YarnFunction("HasItem")]
    public static bool HasItem(string itemName)
    {
        foreach (var item in Instance.inventory)
        {
            if (item.Name == itemName)
            {
                return true;
            }
        }
        return false;
    }

    public void EndSelection()
    {
        SceneManager.LoadScene("Event System");
    }
}
