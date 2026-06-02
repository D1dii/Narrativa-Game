using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
public class YarnScenesManager : MonoBehaviour
{
    [YarnCommand("LoadEquipmentScene")]

    public static void LoadEquipmentScene()
    {
        SceneManager.LoadScene("EquipmentScene");
    }

    public static void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
