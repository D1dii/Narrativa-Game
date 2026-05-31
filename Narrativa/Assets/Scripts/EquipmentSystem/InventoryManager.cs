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
