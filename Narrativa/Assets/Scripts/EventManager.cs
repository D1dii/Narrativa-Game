using UnityEngine;
using Yarn.Unity;

public class EventManager
{

    [YarnFunction("NextEvent")]
    public static string NextEvent()
    {
        return "SecondEvent";
    }

}
