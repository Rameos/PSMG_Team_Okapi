using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour
{

    Color startColor;
    Color currentColor;
    Color endColor;
    Color destColor;
    bool shouldFade = false;
    bool fadeIn = true;
    
    public float fadeSpeed = 0.1f;
    public float startAlpha;

    // Use this for initialization

    void Start()
    {
        startColor = gameObject.renderer.material.color;
        startColor.a = startAlpha;
        renderer.material.color = new Color(startColor.r, startColor.g, startColor.b, startColor.a);
        endColor = new Color(startColor.r, startColor.g, startColor.b, 1.0f);    

        for (int childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
        {
            Transform child = gameObject.transform.GetChild(childIndex);
            child.gameObject.AddComponent<FadeIn>();
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            shouldFade = true;
        }
        
        Fade();
    }

    void Fade()
    {
        if (shouldFade)
        {
            if (fadeIn)
            {
                destColor = endColor;
            }
            else
            {
                destColor = startColor;
            }
            currentColor = Color.Lerp(currentColor, destColor, fadeSpeed);

            gameObject.renderer.material.SetColor("_Color", currentColor);

            if (currentColor == destColor)
            {
                shouldFade = false;
                fadeIn = !fadeIn;
            }         
        }   
    }    
}





