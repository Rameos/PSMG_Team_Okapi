using UnityEngine;
using System.Collections;
using iViewX;

public class InitEyetrackingDebugMock : MonoBehaviour {

    public bool initOnStart = true;

	// Use this for initialization
	void Start () {
        if (initOnStart && !gazeModel.isEyeTrackerRunning)
        {
            GazeControlComponent.Instance.StartCalibration();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
