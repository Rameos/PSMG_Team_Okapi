using UnityEngine;
using System.Collections;

public class EnvironmentVars : MonoBehaviour {

    public static bool restartFromEnding = false;

	void Start () {
        DontDestroyOnLoad(gameObject);
	}
}
