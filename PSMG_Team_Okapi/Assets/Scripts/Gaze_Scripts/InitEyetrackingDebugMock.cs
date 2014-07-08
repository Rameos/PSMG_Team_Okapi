using UnityEngine;
using System.Collections;
using iViewX;

public class InitEyetrackingDebugMock : MonoBehaviour {

    public bool calibrationOnStart = true;

	// Use this for initialization
	void Start () {
        Screen.showCursor = false;
        if (calibrationOnStart && !gazeModel.isEyeTrackerRunning)
        {
            //GazeControlComponent.Instance.StartCalibration();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
