using UnityEngine;
using Yarn.Unity;

public class HardResetMenu : MonoBehaviour
{
    void Start()
    {
        // 1. PURGA AUTOMÁTICA DE YARN SPINNER (Borra el 100% de las variables)
        InMemoryVariableStorage memoriaYarn = FindObjectOfType<InMemoryVariableStorage>();
        if (memoriaYarn != null)
        {
            memoriaYarn.Clear();
        }


        GameObject gestorPartida = GameObject.Find("InventoryManager");
        if (gestorPartida != null)
        {
            Destroy(gestorPartida);
        }
    }
}