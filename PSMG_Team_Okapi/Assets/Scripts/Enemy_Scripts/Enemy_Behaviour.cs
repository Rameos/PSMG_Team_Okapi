using UnityEngine;
using System.Collections;


/*Formel gleichmäßig beschleunigte Bewegung (Geschwindigkeit-Zeit-Gesetz):

v = a · t + v0
"v" ist die Geschwindigkeit in Meter pro Sekunde [m/s]
"a" ist die Beschleunigung in Meter pro Sekunde-Quadrat [ m/s2 ]
"t" ist die Zeit in Sekunden [s]
"v0" ist die Anfangsgeschwindigkeit in Meter pro Sekunde [ m/s ]*/

public class Enemy_Behaviour : MonoBehaviour
{

    public Transform[] patrolpointsIdle; // waypoints
    private Transform[] patrolpointsAlert; // enthält nur Spieler
    private Transform[] patrolpoints; // aktuell verwendete patrolpoints

    public float idlespeed; // geschwindigkeit wenn idle
    public float alertspeed; // geschwindigkeit wenn alert
    public float angryspeed; // geschwindigkeit wenn angry

    public float alertRadius = 20; // radius in dem spieler erkannt wird
    private float eyereach = 16; // sichtweite des gegners (sollte gleich Sichtweite des Spielers sein)

    private float maxStartDistance = 40; // maximale Entfernung zwischen Startpunkt und Geist bevor minPlayerReach erreicht wird
    private float maxPlayerDistance = 30; // Obergrenze des Abstands für die Verfolgung
    private float minPlayerDistance = 25; // Untergrenze des Abstands für die Verfolgung

    public float angryRadius = 8; // Abstand zum Spieler, bei der Geist angry wird
    private float freezetime = 10; // zeit, die gegner eingefroren bleiben soll

    public float idleacceleration = 0.5f;
    public float alertacceleration = 1;
    public float angryacceleration = 1.5f;
    public float pauselength = 0;
    private bool braking = false;

    private bool frozen = false;

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

        agent.speed = idlespeed;
        SetListeners();
    }

    void Update()
    {
        print("speed: " + agent.speed);
        /*print("braking: " + braking);
        print("target distance: " + GetTargetDistance());
        print("braking distance: " + CalcBrakeDistance());*/

        if (patrolpoints.Length > 0 && !frozen)
        {
            Rotate(agent.speed);
            Move();
        }

        switch (enemyState.currentState)
        {
            case Enemy_States.State.idle:
                CheckAlertRadius();
                CheckEyeLine();
                if (!braking)
                {
                    CheckShouldBrake();
                }
                break;
            case Enemy_States.State.alert:
                CheckAngryRadius();
                CheckReach();
                break;
            case Enemy_States.State.angry:
                CheckReach();
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
        if (Util.GetDistance(transform.position, patrolpoints[currentpoint].position) < 3.0f)
        {
            currentpoint++;
            if (currentpoint >= patrolpoints.Length)
            {
                currentpoint = 0;
            }
            braking = false;
            StopCoroutine("Brake");
            AccelerateToIdle();
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


    // dynamische Geschwindigkeit

    private void AccelerateToAlert()
    {
        float[] args = new float[] { alertspeed, alertacceleration };
        StartCoroutine("Accelerate", args);
    }

    private void AccelerateToAngry()
    {
        float[] args = new float[] { angryspeed, angryacceleration };
        StartCoroutine("Accelerate", args);
    }

    private void BrakeToIdle()
    {
        float[] args = new float[]{idlespeed, angryacceleration};
        StartCoroutine("Brake", args);
    }

    private void BrakeToStop()
    {
        float[] args = new float[] { 0, idleacceleration };
        StartCoroutine("Brake", args);
    }

    private void AccelerateToIdle()
    {
        float[] args = new float[] { idlespeed, idleacceleration };
        StartCoroutine("Accelerate", args);
    }


    IEnumerator Accelerate(float[] args)
    {
        float startSpeed = agent.speed;
        float targetSpeed = args[0];
        float acceleration = args[1];
        int time = 0;

        while (agent.speed < targetSpeed)
        {
            yield return new WaitForSeconds(1);
            agent.speed = acceleration * time * time + startSpeed;
            time++;
        }
    }

    IEnumerator Brake(float[] args)
    {
        int time = 0;
        float startSpeed = agent.speed;
        float targetSpeed = args[0];
        float acceleration = args[1];

        while (agent.speed > targetSpeed)
        {
            yield return new WaitForSeconds(1);
            agent.speed = startSpeed - acceleration * time * time;
            time++;
        }
    }

    IEnumerator FastBrake(float targetSpeed)
    {
        int time = 0;
        float startSpeed = agent.speed;

        while (agent.speed > targetSpeed)
        {
            yield return new WaitForSeconds(1);
            agent.speed = startSpeed - angryacceleration * time * time;
            time++;
        }
    }

    private void CheckShouldBrake()
    {
        if (CalcBrakeDistance() >= GetTargetDistance())
        {
            braking = true;
            BrakeToStop();
        }
    }

    private float CalcBrakeDistance()
    {
        return ((agent.speed * agent.speed) / (2 * idleacceleration));
    }

    private float GetTargetDistance()
    {
        Vector3 enemyPosition = transform.position;
        Vector3 targetPosition = patrolpoints[currentpoint].position;

        return Util.GetDistance(enemyPosition, targetPosition);
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
        //SetAlertSpeed();
    }

    IEnumerator Schrecksekunde()
    {
        yield return new WaitForSeconds(pauselength);
        SetAlertSpeed();
    }

    public void SetPatrolpointsAlert()
    {
        patrolpoints = patrolpointsAlert;
    }

    public void SetIdleSpeed()
    {
        agent.speed = idlespeed;
        //BrakeToIdle();
    }

    public void SetAlertSpeed()
    {
        //agent.speed = alertspeed;
        frozen = false;
        AccelerateToAlert();
    }

    public void SetAngrySpeed()
    {
        //agent.speed = angryspeed;
        AccelerateToAngry();
    }

    public void SetNoSpeed()
    {
        agent.speed = 0.00f;
    }

    public void SetFrozen()
    {
        StopCoroutine("Brake");
        StopCoroutine("Accelerate");
        frozen = true;
        agent.speed = 0;

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
        enemyState.OnFreeze += SetFrozen;
        enemyState.OnIdle += SetPatrolpointsIdle;
        enemyState.OnIdle += SetIdleSpeed;
    }
}
