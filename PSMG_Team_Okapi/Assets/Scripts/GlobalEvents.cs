using UnityEngine;
using System.Collections;

public class GlobalEvents : MonoBehaviour {

    public delegate void DefaultEventHandler();

    public static event DefaultEventHandler OnPlayerDeath = delegate { };
    public static event DefaultEventHandler OnScreenFadeInComplete = delegate { };
    public static event DefaultEventHandler OnScreenFadeOutComplete = delegate { };


    // Triggers

    public static void TriggerOnPlayerDeath()
    {
        OnPlayerDeath();
    }
    public static void TriggerOnScreenFadeInComplete()
    {
        OnScreenFadeInComplete();
    }

    public static void TriggerOnScreenFadeOutComplete()
    {
        OnScreenFadeOutComplete();
    }
}
