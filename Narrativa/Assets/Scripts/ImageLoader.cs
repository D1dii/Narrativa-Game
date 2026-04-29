using UnityEngine;
using Yarn.Unity;

public class ImageLoader : MonoBehaviour
{

    [SerializeField] private Canvas canvasToActivate;

    public static Canvas myCanvas;

    private void Awake()
    {
        myCanvas = canvasToActivate;
    }

    [YarnCommand("ShowImageOnScreen")]
    public static void ShowHiddenCanvas()
    {
        if (myCanvas != null)
        {
            Debug.Log("Activate Canvas");
            myCanvas.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Canvas reference is missing!");
        }

    }
}