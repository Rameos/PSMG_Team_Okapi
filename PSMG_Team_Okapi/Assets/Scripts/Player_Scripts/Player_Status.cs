using UnityEngine;
using System.Collections.Generic;

public class Player_Status  : MonoBehaviour
{
    public enum MovementState { none, walk, traverse }
    public MovementState movementState = MovementState.none;

    private float vertAxis;
    private float horizAxis;

    public bool inputEnabled = true;

    public List<GameObject> enemiesChasing;

    void Start()
    {
        enemiesChasing = new List<GameObject>();
    }

    void Update()
    {
        if (!inputEnabled)
        {
            setState("none");
            return;
        }

        vertAxis = Input.GetAxis("Vertical");
        horizAxis = Input.GetAxis("Horizontal");

        if(vertAxis == 0 && horizAxis == 0)
        {
            setState("none");
        }
        else if (vertAxis != 0)
        {
            setState("walk");
        }
        else if (vertAxis == 0 && horizAxis != 0)
        {
            setState("traverse");
        }         
    }

    private void setState(string state)
    {
        switch(state)
        {
            case "none":
                {
                    movementState = MovementState.none;
                    break;
                }
            case "walk":
                {
                    movementState = MovementState.walk;
                    break;
                }
            case "traverse":
                {
                    movementState = MovementState.traverse;
                    break;
                }
        }
    }

    public string getState()
    {
        return movementState.ToString();
    }
}
