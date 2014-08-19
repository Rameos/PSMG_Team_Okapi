using UnityEngine;
using System.Collections;

public class Enemy_Behaviour : MonoBehaviour
{

    public Transform[] patrolpointsIdle; // reihe von waypoints
    public Transform[] patrolpointsAlert; // enthält erstmal nur Spieler
    private Transform[] patrolpoints;

    public float idlespeed = 2; // geschwindigkeit wenn idle
    public float alertspeed = 3; // geschwindigkeit wenn alert
    public float angryspeed = 5; // geschwindigkeit wenn angry
	private float startspeed;
	private float targetspeed;
	public float acceleration = 1f;
	private int time;
    private bool braking;

    public float alertRadius = 12; // radius in dem spieler erkannt wird
    private float eyereach = 16; // sichtweite des gegners (sollte gleich Sichtweite des Spielers sein)

    private float maxStartDistance = 20; // maximale Entfernung zwischen Startpunkt und Geist bevor minPlayerReach erreicht wird
    public float maxPlayerDistance = 15; // Obergrenze des Abstands für die Verfolgung
    public float minPlayerDistance = 13; // Untergrenze des Abstands für die Verfolgung

    public float angryRadius = 6; // Abstand zum Spieler, bei der Geist angry wird

    private int currentpoint;
    private Vector3 homepoint;

    private GameObject player;

    private NavMeshAgent agent;
    private Enemy_States enemyState;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        
        patrolpointsAlert = new Transform[1] { player.transform };
       
        patrolpoints = patrolpointsIdle;
        currentpoint = 0;

        enemyState = gameObject.GetComponent<Enemy_States>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        targetspeed = idlespeed;
        time = 0;
        braking = false;

        SetListeners();
    }

    void Update()
    {
        if (patrolpoints.Length > 0)
        {
            Rotate(agent.speed);
            Move();
        }

        switch (enemyState.currentState)
        {
            case Enemy_States.State.idle:
                CheckAlertRadius();
                CheckEyeLine();
                //Accelerate();
                break;
            case Enemy_States.State.alert:
                CheckAngryRadius();
                CheckReach();
				//Accelerate();
                break;
            case Enemy_States.State.angry:
                CheckReach();
				//Accelerate();
                break;
        }
    }

    private void CheckAlertRadius()
    {
        if (GetPlayerDistance() < alertRadius)
        {
            enemyState.Alert();
        }
    }

    private void CheckEyeLine() // Wenn Gegner Spieler sieht, soll Gegner alert werden
    {
        Vector3 target = player.transform.position;
        //float step = 2 * agent.speed * Time.deltaTime;

        Vector3 v1 = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 v2 = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z);

        if (Vector3.Angle(v1, v2) < 5 && GetPlayerDistance() <= eyereach) // if player in front of enemy
        {
            //print("player was hit");
            enemyState.Alert();
        }

    }

    private void CheckReach() // überprüft ob Verfolgung abgebrochen werden kann
    {
        if (GetPlayerDistance() > GetReach())
        {
            enemyState.Idle();
        }
    }

    private float GetReach() // Reach wird bei größerem Abstand zum homepoint kleiner.
    {
        float leeway = maxPlayerDistance - minPlayerDistance;
        float distance = Util.GetDistance(homepoint, transform.position);
        float percentage = distance / maxStartDistance;

        return minPlayerDistance + leeway * percentage;
    }

    private void CheckAngryRadius() // überprüft ob wechsel zu Angry stattfinden soll
    {
        if (GetPlayerDistance() <= angryRadius)
        {
            enemyState.Angry();
        }
    }

    private void Move() // überprüft ob aktuelles Ziel erreicht wurde und setzt ggf. neues Ziel
    {
        if (GetTargetDistance() < 3.0f)
        {
            braking = false;
            time = 0;
            currentpoint++;
            if (currentpoint >= patrolpoints.Length)
            {
                currentpoint = 0;
            }
        }

        if (GetTargetDistance() < GetBrakeDistance())
        {
            braking = true;
            time = 0;
        }

        agent.SetDestination(patrolpoints[currentpoint].position);
    }

    private void Rotate(float speed)
    {
        if (speed == 0 && enemyState.currentState == Enemy_States.State.alert) speed = angryspeed;

        Vector3 target = patrolpoints[currentpoint].position;
        float step = 2 * speed * Time.deltaTime;

        Vector3 v1 = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 v2 = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z);

        if (Vector3.Angle(v1, v2) > 0.5) // if not facing front
        {
            Vector3 newDir = Vector3.RotateTowards(v1, v2, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir); // rotate to front
        }
    }

    private float GetPlayerDistance()
    {
        Vector3 enemyPosition = transform.position;
        Vector3 playerPosition = player.transform.position;        

        return Util.GetDistance(enemyPosition, playerPosition);
    }

    private float GetTargetDistance()
    {
        Vector3 enemyPosition = transform.position;
        Vector3 targetPosition = patrolpoints[currentpoint].position;

        return Util.GetDistance(enemyPosition, targetPosition);
    }

    private float GetBrakeDistance()
    {
        return (agent.speed * agent.speed) / acceleration;
    }

    private void Brake()
    {
        agent.speed = Mathf.Max(startspeed - acceleration * time * time, 0);
    }

	private void Accelerate()
	{
        /*if (braking)
        {
            Brake();
            return;
        }*/

		if(startspeed < targetspeed)
		{
			agent.speed = Mathf.Min(startspeed + acceleration * time * time, targetspeed);
		}
		else 
		{
			agent.speed = Mathf.Max(startspeed - acceleration * time * time, targetspeed);
		}
		time++;
	}

    // state listeners

    public void SetPatrolpointsIdle()
    {
        patrolpoints = patrolpointsIdle;
    }

    public void Pause()
    {
        SetNoSpeed();
        StartCoroutine(Schrecksekunde());
    }

    IEnumerator Schrecksekunde()
    {
        yield return new WaitForSeconds(1);
        SetAlertSpeed();
    }

    public void SetPatrolpointsAlert()
    {
        patrolpoints = patrolpointsAlert;
    }

    public void SetIdleSpeed()
    {
		startspeed = agent.speed;
		targetspeed = idlespeed;
        time = 0;
    }

    public void SetAlertSpeed()
    {
		startspeed = agent.speed;
		targetspeed = alertspeed;
        time = 0;
    }

    public void SetAngrySpeed()
    {
        startspeed = agent.speed;
		targetspeed = angryspeed;
        time = 0;
    }

    public void SetNoSpeed()
    {
        startspeed = 0;
		targetspeed = 0;
        time = 0;
    }

    public void ResetCurrentpoint()
    {
        currentpoint = 0;
    }

    public void SetHomePoint()
    {
        homepoint = transform.position;
    }


    private void SetListeners()
    {
        enemyState.OnAlert += Pause;
        enemyState.OnAlert += SetPatrolpointsAlert;
        enemyState.OnAlert += ResetCurrentpoint;
        enemyState.OnAlert += SetHomePoint;

        enemyState.OnAngry += SetAngrySpeed;

        enemyState.OnFreeze += SetNoSpeed;

        enemyState.OnIdle += SetPatrolpointsIdle;
        enemyState.OnIdle += SetIdleSpeed;
    }
}
