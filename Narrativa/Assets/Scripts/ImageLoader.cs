using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using System.Collections.Generic;

public class ImageLoader : MonoBehaviour
{
    [System.Serializable]
    public struct DialogueSprite
    {
        public string spriteName;    
        public Sprite sprite;      
    }

    [Header("Referencias UI")]
    [SerializeField] private GameObject imageContainer; 
    [SerializeField] private Image characterImage;      
    [Header("Configuración de Sprites")]
    [SerializeField] private List<DialogueSprite> sprites = new List<DialogueSprite>();

    private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();

    private static ImageLoader instance;

    private void Awake()
    {
        instance = this;

        foreach (var s in sprites)
        {
            if (!spriteDictionary.ContainsKey(s.spriteName))
            {
                spriteDictionary.Add(s.spriteName, s.sprite);
            }
        }

        if (imageContainer != null)
        {
            imageContainer.SetActive(false);
        }
    }

    [YarnCommand("ShowSprite")]
    public static void ShowSprite(string spriteName)
    {
        if (instance == null) return;

        if (instance.spriteDictionary.TryGetValue(spriteName, out Sprite spriteToShow))
        {
            instance.characterImage.sprite = spriteToShow;
            instance.imageContainer.SetActive(true);
            Debug.Log($"Sprite cargado: {spriteName}");
        }
        else
        {
            Debug.LogWarning($"¡No se encontró el sprite '{spriteName}' en la lista del Inspector!");
        }
    }

    [YarnCommand("HideSprite")]
    public static void HideSprite()
    {
        if (instance == null) return;

        if (instance.imageContainer != null)
        {
            instance.imageContainer.SetActive(false);
            Debug.Log("Sprite oculto");
        }
    }
}