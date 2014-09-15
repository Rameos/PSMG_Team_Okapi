using UnityEngine;
using System.Collections;

public class SkipLevelHelper : MonoBehaviour {

    private string levelName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            SkipToLevel("Level_two");
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            SkipToLevel("generator_room");
        }
	}

    private void SkipToLevel(string levelName)
    {
        this.levelName = levelName;

        GlobalEvents.OnScreenFadeOutComplete += OnFadeOutComplete;
        ScreenFader.Instance.FadeToBlack();
    }

    private void OnFadeOutComplete()
    {
        Application.LoadLevel(levelName);
    }

    void OnDestroy()
    {
        GlobalEvents.OnScreenFadeOutComplete -= OnFadeOutComplete;
    }
}
