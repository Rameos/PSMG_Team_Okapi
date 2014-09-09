using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour {

    public float fadeSpeed = 1.5f;
    public bool debug = false;

    private bool fading = true;
    private Color targetColor;

    private static ScreenFader instance;

    public static ScreenFader Instance
    {
        get
        {
            if (!instance)
            {
                instance = (ScreenFader)FindObjectOfType(typeof(ScreenFader));
                {
                    if (!instance)
                    {
                        Debug.LogError("Couldn't find ScreenFader");
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

        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);

        FadeToClear();
    }


    void Update()
    {
        if (fading)
        {
            CalcFadedColor();
        }   
    }


    public void FadeToClear()
    {
        StartFading(Color.clear);
    }


    public void FadeToBlack()
    {
        if (debug)
        {
            Debug.Log("Starting FadeOut Black");
        }        
        StartFading(Color.black);
    }

    public void FadeToWhite()
    {
        if (debug)
        {
            Debug.Log("Starting FadeOut White");
        }        
        StartFading(Color.white);
    }

    private void StartFading(Color targetColor)
    {
        this.targetColor = targetColor;
        guiTexture.enabled = true;
        fading = true;
    }

    private void CalcFadedColor()
    {
        guiTexture.color = Color.Lerp(guiTexture.color, targetColor, fadeSpeed * Time.deltaTime);        

        if (Mathf.Abs(guiTexture.color.a - targetColor.a) < (0.02f / fadeSpeed))
        {
            if (debug)
            {
                Debug.Log("Fading completed!");
            }            
            fading = false;

            BroadcastCompletion();

            guiTexture.color = targetColor;            
        }
    }

    private void BroadcastCompletion()
    {
        if (targetColor == Color.clear)
        {
            GlobalEvents.TriggerOnScreenFadeInComplete();
        }
        else if(targetColor != Color.clear)
        {
            GlobalEvents.TriggerOnScreenFadeOutComplete();
        }
        else
        {
            Debug.LogError("Unspecified targetColor in ScreenFader!");
        }
    }
}
