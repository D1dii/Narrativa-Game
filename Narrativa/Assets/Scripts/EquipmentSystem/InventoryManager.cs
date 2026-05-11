using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;
    public List<IBaseItem> inventory = new List<IBaseItem>();

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
        inventory.Add(item);
    }

    public void RemoveItem(IBaseItem item)
    {
        inventory.Remove(item);
    }

    public void EndSelection()
    {
        SceneManager.LoadScene("Event System");
    }
}
