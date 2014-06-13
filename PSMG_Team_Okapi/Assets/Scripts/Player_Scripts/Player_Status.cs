using UnityEngine;
using System.Collections;

public class Player_Status  : MonoBehaviour
{
    public enum MovementState { none, walk, traverse}
    public MovementState movementState = MovementState.none;

    private float vertAxis;
    private float horizAxis;

    public delegate void HealthChangeHandler(int newHealth);
    public event HealthChangeHandler OnHealthChanged = delegate { };

    private int playerHealth;

    void Start()
    {
        playerHealth = 100;
    }

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

        // Debugging
        if (Input.GetKeyDown(KeyCode.K))
        {
            this.health = 0;
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

    public int health
    {
        get
        {
            return playerHealth;
        }
        set
        {
            int previousHealth = playerHealth;

            playerHealth = Mathf.Max(0, Mathf.Min(100, value));

            if (playerHealth != previousHealth)
            {
                Debug.Log(playerHealth);
                OnHealthChanged(playerHealth);

                if (playerHealth <= 0)
                {
                    GlobalEvents.TriggerOnPlayerDeath();
                }
            }
        }
    }       
}
