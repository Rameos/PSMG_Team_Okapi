using UnityEngine;
using System.Collections;

public class Player_Health : MonoBehaviour {

    public delegate void HealthChangeHandler(int newHealth, int oldHealth);
    public event HealthChangeHandler OnHealthChanged = delegate { };

    private int playerHealth;

    public float regenerationHealthPerSecond = 3;
    public float regenerationDelaySeconds = 5;
    public bool isRegenerating = false;
    private float timeOfLastHealthLoss = 0;

    private Player_Status playerStatus;

	// Use this for initialization
	void Start () {
        playerHealth = 100;

        playerStatus = gameObject.GetComponent<Player_Status>();

        OnHealthChanged += HealthChangeListener;
	}
	
	// Update is called once per frame
	void Update () {
        // Debugging
        if (Input.GetKeyDown(KeyCode.K))
        {
            this.health = 0;
        }
	}

    void FixedUpdate()
    {
        if (!isRegenerating && playerHealth < 100 && Time.fixedTime > timeOfLastHealthLoss + regenerationDelaySeconds) // have we lost no health for long enough?
        {
            if (playerStatus.enemiesChasing.Count == 0) // are we not being chased?
            {
                //Debug.Log("Starting Regeneration!");
                StartCoroutine(RegenerateHealth());
            }
        }        
    }

    IEnumerator RegenerateHealth()
    {
        isRegenerating = true;

        while (isRegenerating)
        {
            health++;

            if (health == 100)
            {
                //Debug.Log("Stopping Regeneration!");
                isRegenerating = false;
            }

            yield return new WaitForSeconds(1.0f / regenerationHealthPerSecond);
        }        
    }    

    void HealthChangeListener(int newHealth, int oldHealth)
    {
        if (newHealth < oldHealth)
        {
            Debug.Log(playerHealth);
            isRegenerating = false;
            timeOfLastHealthLoss = Time.fixedTime;
        }

        //Debug.Log(playerHealth);
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
                OnHealthChanged(playerHealth, previousHealth);

                if (playerHealth <= 0)
                {
                    GlobalEvents.TriggerOnPlayerDeath();
                }
            }
        }
    }       
}
