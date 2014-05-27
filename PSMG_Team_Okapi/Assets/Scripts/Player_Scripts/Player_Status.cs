using UnityEngine;
using System.Collections;

public class Player_Status  : MonoBehaviour
{

    public enum MovementState { none, walk, traverse}
    public static int playerHealth;

    public MovementState movementState = MovementState.none;

    private float vertAxis;
    private float horizAxis;

    void Update()
    {

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
