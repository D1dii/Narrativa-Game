using UnityEngine;
using Yarn.Unity;

public class ImageLoader : MonoBehaviour
{
    public static GameObject myCanvas;

    [YarnCommand("ShowImageOnScreen")]
    public static void ShowHiddenCanvas()
    {
        if (myCanvas != null)
        {
            myCanvas.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Canvas reference is missing!");
        }

        Debug.Log("Hola");
    }
}