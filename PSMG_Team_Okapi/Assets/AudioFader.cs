using UnityEngine;
using System.Collections;

public class AudioFader : MonoBehaviour {

    public float fadeSpeed = 20f;
    public bool debug = false;

    private bool fading = true;
    private float targetVolume;

    private static AudioFader instance;

    public static AudioFader Instance
    {
        get
        {
            if (!instance)
            {
                instance = (AudioFader)FindObjectOfType(typeof(AudioFader));
                {
                    if (!instance)
                    {
                        Debug.LogError("Couldn't find AudioFader");
                    }
                }
            }

            return instance;
        }
    }

    void OnLevelWasLoaded()
    {
        if (EnvironmentVars.restartFromEnding)
        {
            guiTexture.color = Color.white;
        }
    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        FadeIn();
    }


    void Update()
    {
        print(AudioListener.volume);
        if (fading)
        {
            CalcFadedColor();
        }   
    }


    public void FadeOut()
    {
        StartFading(0);
    }


    public void FadeIn()
    {
        StartFading(100);
    }


    private void StartFading(float targetVolume)
    {
        this.targetVolume = targetVolume;
        fading = true;
    }

    private void CalcFadedColor()
    {
        AudioListener.volume = Mathf.Lerp(AudioListener.volume, targetVolume, fadeSpeed * Time.deltaTime);        

        if (Mathf.Abs(AudioListener.volume - targetVolume) < (0.02f / fadeSpeed))
        {
            if (debug)
            {
                Debug.Log("Fading completed!");
            }            
            fading = false;

            BroadcastCompletion();

            AudioListener.volume = targetVolume;            
        }
    }

    private void BroadcastCompletion()
    {
        if (targetVolume == 0)
        {
            GlobalEvents.TriggerOnAudioFadeOutComplete();
        }
        else if(targetVolume != 0)
        {
            GlobalEvents.TriggerOnAudioFadeInComplete();
        }
        else
        {
            Debug.LogError("Unspecified target in AudioFader!");
        }
    }
}