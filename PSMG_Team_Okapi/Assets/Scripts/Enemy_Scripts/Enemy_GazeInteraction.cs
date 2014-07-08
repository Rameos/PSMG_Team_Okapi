using UnityEngine;
using System.Collections;
using iViewX;

public class Enemy_GazeInteraction : MonoBehaviourWithGazeComponent
{

    public delegate void EnemyGazeHandler();

    public event EnemyGazeHandler OnEnemyGazeEntered = delegate { };
    public event EnemyGazeHandler OnEnemyGazeStay = delegate { };
    public event EnemyGazeHandler OnEnemyGazeExited = delegate { };

    public bool debug = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnGazeEnter(RaycastHit hit)
    {
        OnEnemyGazeEntered();
        
        if (debug)
        {
            Debug.Log("GazeEntered");        
        }
    }
    
    public override void OnGazeStay(RaycastHit hit)
    {
        OnEnemyGazeStay();
    }
        
    public override void OnGazeExit()
    {        
        OnEnemyGazeExited();

        if (debug)
        {
            Debug.Log("GazeExit");
        }
    }
}
