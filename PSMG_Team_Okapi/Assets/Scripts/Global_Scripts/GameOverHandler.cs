using UnityEngine;
using System.Collections;

public class GameOverHandler : MonoBehaviour {

    private bool reloadAfterScreenFadeOut = false;
    
	// Use this for initialization
	void Start () {
        GlobalEvents.OnPlayerDeath += OnPlayerDeath;
        GlobalEvents.OnScreenFadeOutComplete += OnScreenFadeOutComplete;
	}

    private void OnPlayerDeath()
    {
        // pause, fade out, reload level
        Debug.Log("Player died!");

        reloadAfterScreenFadeOut = true;
        ScreenFader.Instance.FadeToBlack();
        AudioFader.Instance.FadeOut();
    }

    private void OnScreenFadeOutComplete()
    {
        if (reloadAfterScreenFadeOut)
        {
            Debug.Log("Reloading Level!");
            reloadAfterScreenFadeOut = false;
            LevelLoader.ReloadCurrentLevel();
        }
    }

    void OnDestroy()
    {
        GlobalEvents.OnPlayerDeath -= OnPlayerDeath;
        GlobalEvents.OnScreenFadeOutComplete -= OnScreenFadeOutComplete;
    }
}
