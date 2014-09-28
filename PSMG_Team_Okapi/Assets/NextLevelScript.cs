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
        AudioFader.Instance.FadeOut();
    }

    void OnScreenFadeOutComplete()
    {
        LevelLoader.LoadLevel(nextLevel);
    }

    void OnDestroy()
    {
        GlobalEvents.OnScreenFadeOutComplete -= OnScreenFadeOutComplete;
    }
}
