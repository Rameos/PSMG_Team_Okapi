using UnityEngine;
using System.Collections;

public class Player_Health : MonoBehaviour {

    public delegate void HealthChangeHandler(int newHealth);
    public event HealthChangeHandler OnHealthChanged = delegate { };

    private int playerHealth;

	// Use this for initialization
	void Start () {
        playerHealth = 100;
	}
	
	// Update is called once per frame
	void Update () {
        // Debugging
        if (Input.GetKeyDown(KeyCode.K))
        {
            this.health = 0;
        }   
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
