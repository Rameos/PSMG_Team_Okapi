using UnityEngine;
using System.Collections;
using iViewX;

public class Enemy_GazeInteraction : MonoBehaviourWithGazeComponent
{

    public delegate void EnemyGazeHandler();

    public event EnemyGazeHandler OnEnemyGazeEntered;
    public event EnemyGazeHandler OnEnemyGazeExited;    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnGazeEnter(RaycastHit hit)
    {
        //Debug.Log("GazeEntered");        
        OnEnemyGazeEntered();
    }
    
    public override void OnGazeStay(RaycastHit hit)
    {
        
    }
        
    public override void OnGazeExit()
    {
        //Debug.Log("GazeExit");        
        OnEnemyGazeExited();
    }
}
