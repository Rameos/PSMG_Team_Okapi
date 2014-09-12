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
            Application.LoadLevel(nextLevelIndex);
        }
        else
        {
            Debug.LogError(string.Format("Error trying to load next level. Last Level is already loaded!"));
        }
    }

    public static void ReloadCurrentLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public static void LoadLevel(int index)
    {
        Application.LoadLevel(index);
    }

    public static void LoadLevel(string name)
    {
        Application.LoadLevel(name);
    }
}
