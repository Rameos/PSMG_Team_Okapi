using UnityEngine;
using System.Collections;
using iViewX;

public class Enemy_GazeInteraction : MonoBehaviourWithGazeComponent
{

    public delegate void OnEnemyGazeEntered();
    public event OnEnemyGazeEntered EnemyGazeEntered;

    public delegate void OnEnemyGazeExited();
    public event OnEnemyGazeExited EnemyGazeExited;    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnGazeEnter(RaycastHit hit)
    {
        //Debug.Log("GazeEntered");        
        EnemyGazeEntered();
    }
    
    public override void OnGazeStay(RaycastHit hit)
    {
        
    }
        
    public override void OnGazeExit()
    {
        //Debug.Log("GazeExit");        
        EnemyGazeExited();
    }
}
