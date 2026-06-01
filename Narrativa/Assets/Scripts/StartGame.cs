using UnityEngine;
using Yarn.Unity;

public class StartGame : MonoBehaviour
{
    [Header("Referencias")]
    public DialogueRunner dialogueRunner;

    [Header("Configuración")]
    public string nodoInicial = "Event_1";

    void Start()
    {
        if (!dialogueRunner.IsDialogueRunning)
        {
            dialogueRunner.StartDialogue(nodoInicial);
        }
    }
}