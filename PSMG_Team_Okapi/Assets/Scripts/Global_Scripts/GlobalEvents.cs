using UnityEngine;
using System.Collections;

public class GlobalEvents : MonoBehaviour {

    public delegate void DefaultEventHandler();

    public static event DefaultEventHandler OnPlayerDeath = delegate { };
    public static event DefaultEventHandler OnScreenFadeInComplete = delegate { };
    public static event DefaultEventHandler OnScreenFadeOutComplete = delegate { };

    public static event DefaultEventHandler OnAudioFadeInComplete = delegate { };
    public static event DefaultEventHandler OnAudioFadeOutComplete = delegate { };

    public static event DefaultEventHandler OnPause = delegate { };
    public static event DefaultEventHandler OnUnPause = delegate { };
    
    public static void Reset()
    {
        OnPlayerDeath = delegate { };
        OnScreenFadeInComplete = delegate { };
        OnScreenFadeOutComplete = delegate { };
        OnAudioFadeInComplete = delegate { };
        OnAudioFadeOutComplete = delegate { };
        OnPause = delegate { };
        OnUnPause = delegate { };
    }

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

    public static void TriggerOnAudioFadeInComplete()
    {
        OnScreenFadeInComplete();
    }

    public static void TriggerOnAudioFadeOutComplete()
    {
        OnScreenFadeOutComplete();
    }

    public static void TriggerOnPause()
    {
        OnPause();
    }

    public static void TriggerOnUnPause()
    {
        OnUnPause();
    }
}
