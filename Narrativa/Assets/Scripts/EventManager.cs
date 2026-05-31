using UnityEngine;
using Yarn.Unity;

public class EventManager
{

    [YarnFunction("NextEvent")]
    public static string NextEvent(int currentAct)
    {
        int randomEvent = -1;
        switch (currentAct)
        {
            case 1:
                randomEvent = Random.Range(2, 6);
                break;
            case 2:
                randomEvent = Random.Range(6, 11);
                break;
            case 3:
                randomEvent = Random.Range(11, 16);
                break;
        }
            

        return "Event_" + randomEvent;
    }

    [YarnFunction("HasSuccess")]
    public static bool HasSuccess(int percentageOfSuccess)
    {
        int randomValue = Random.Range(0, 100);
        return randomValue < percentageOfSuccess;
    }

}
