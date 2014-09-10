using UnityEngine;
using System.Collections;
using iViewX;

public class InkubatorEnemy_GazeInteraction : MonoBehaviourWithGazeComponent {

    public bool debug = false;

    public delegate void InkubatorEnemyGazeHandler();
    public event InkubatorEnemyGazeHandler OnEnemyGazeEntered = delegate { };
    public event InkubatorEnemyGazeHandler OnEnemyGazeStay = delegate { };
    public event InkubatorEnemyGazeHandler OnEnemyGazeExited = delegate { };

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnGazeEnter(RaycastHit hit)
    {
        if (debug)
        {
            Debug.Log("GazeEnter");
        }
        OnEnemyGazeEntered();
    }

    public override void OnGazeStay(RaycastHit hit)
    {
        OnEnemyGazeStay();
    }

    public override void OnGazeExit()
    {
        if (debug)
        {
            Debug.Log("GazeExit");
        }
        OnEnemyGazeExited();
    }
}
