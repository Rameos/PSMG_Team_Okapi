using UnityEngine;
using System.Collections;

public class NextLevelScript : MonoBehaviour {

    public string nextLevel = "";

	void Start () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (nextLevel == "")
        {
            Debug.LogError("nextLevel variable not set!");
            return;
        }

        GlobalEvents.OnScreenFadeOutComplete += OnScreenFadeOutComplete;
        ScreenFader.Instance.FadeToBlack();
    }

    void OnScreenFadeOutComplete()
    {
        Application.LoadLevel(nextLevel);
    }

    void OnDestroy()
    {
        GlobalEvents.OnScreenFadeOutComplete -= OnScreenFadeOutComplete;
    }
}
