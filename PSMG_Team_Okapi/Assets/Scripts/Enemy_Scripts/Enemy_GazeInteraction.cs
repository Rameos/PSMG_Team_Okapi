using UnityEngine;
using System.Collections;
using iViewX;

public class Enemy_GazeInteraction : MonoBehaviourWithGazeComponent
{

    private Enemy_TransparencyController transparencyController;

	// Use this for initialization
	void Start () {
        transparencyController = gameObject.transform.parent.GetComponent<Enemy_TransparencyController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnGazeEnter(RaycastHit hit)
    {
        Debug.Log("GazeEntered");
        transparencyController.GazeEnter();
    }
    
    public override void OnGazeStay(RaycastHit hit)
    {
        
    }
        
    public override void OnGazeExit()
    {
        Debug.Log("GazeExit");
        transparencyController.GazeExit();
    }
}
