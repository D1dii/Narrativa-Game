using UnityEngine;
using Yarn.Unity;

public class EventManager : MonoBehaviour
{

    [YarnCommand]
    public void NextEvent()
    {
        Debug.Log("Next Event!");
    }

}
