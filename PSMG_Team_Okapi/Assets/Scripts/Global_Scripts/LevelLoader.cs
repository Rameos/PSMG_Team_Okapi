using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        Screen.showCursor = false;
    }

    public static void LoadNextLevel()
    {
        int nextLevelIndex = Application.loadedLevel + 1;

        if (nextLevelIndex < Application.levelCount)
        {
            GlobalEvents.Reset();
            Application.LoadLevel(nextLevelIndex);
        }
        else
        {
            Debug.LogError(string.Format("Error trying to load next level. Last Level is already loaded!"));
        }
    }

    public static void ReloadCurrentLevel()
    {
        GlobalEvents.Reset();
        Application.LoadLevel(Application.loadedLevel);
    }

    public static void LoadLevel(int index)
    {
        GlobalEvents.Reset();
        Application.LoadLevel(index);
    }

    public static void LoadLevel(string name)
    {
        GlobalEvents.Reset();
        Application.LoadLevel(name);
    }
}
