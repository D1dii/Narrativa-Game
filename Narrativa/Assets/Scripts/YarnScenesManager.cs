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

    [YarnCommand("LoadMenuScene")]

    public static void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
    [YarnCommand("CargarEscena")]
    public void CargarEscena(string nombreDeLaEscena)
    {
        SceneManager.LoadScene(nombreDeLaEscena);
    }
}
