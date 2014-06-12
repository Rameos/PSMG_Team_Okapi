using UnityEngine;
using System.Collections;

public class Enemy_Behaviour : MonoBehaviour
{

    public Transform[] patrolpointsIdle; // reihe von waypoints
    public Transform[] patrolpointsAlert; // enthält erstmal nur Spieler
    private Transform[] patrolpoints;

    public float idlespeed = 1; // geschwindigkeit wenn idle oder alert
    public float alertspeed = 2;
    public float angryspeed = 3; // geschwindigkeit wenn angry

    public float alertRadius = 12; // radius in dem spieler erkannt wird
    private float eyereach = 16; // sichtweite des gegners (sollte gleich Sichtweite des Spielers sein)

    private float maxStartDistance = 20; // maximale Entfernung zwischen Startpunkt und Geist bevor minPlayerReach erreicht wird
    public float maxPlayerDistance = 15; // Obergrenze des Abstands für die Verfolgung
    public float minPlayerDistance = 13; // Untergrenze des Abstands für die Verfolgung

    public float angryRadius = 6; // Abstand zum Spieler, bei der Geist angry wird
    private float freezetime = 10; // zeit, die gegner eingefroren bleiben soll

    private int currentpoint;
    private Vector3 homepoint;

    private NavMeshAgent agent;
    private Enemy_States states;

    void Start()
    {
        if (patrolpointsAlert.Length == 0)
        {
            patrolpointsAlert = new Transform[1] { GameObject.Find("Player").transform };
        }

        patrolpoints = patrolpointsIdle;
        currentpoint = 0;

        states = gameObject.GetComponent<Enemy_States>();
        agent = gameObject.GetComponent<NavMeshAgent>();

        agent.speed = idlespeed;

        SetListeners();
    }

    void Update()
    {
        if (patrolpoints.Length > 0)
        {
            Rotate();
            Move();
        }

        switch (states.getState())
        {
            case Enemy_States.States.idle:
                CheckAlertRadius();
                CheckEyeLine();
                break;
            case Enemy_States.States.alert:
                CheckAngryRadius();
                CheckReach();
                break;
            case Enemy_States.States.angry:
                CheckReach();
                break;
        }
    }

    private void CheckAlertRadius()
    {
        if (GetPlayerDistance() < alertRadius)
        {
            states.AlertGhost();
        }
    }

    private void CheckEyeLine() // Wenn Gegner Spieler sieht, soll Gegner alert werden
    {
        Vector3 target = GameObject.Find("Player").transform.position;
        float step = 2 * agent.speed * Time.deltaTime;

        Vector3 v1 = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 v2 = new Vector3(target.x - transform.position.x, 0, target.z - transform.position.z);

        if (Vector3.Angle(v1, v2) < 5 && GetPlayerDistance() <= eyereach) // if player in front of enemy
        {
            //print("player was hit");
            states.AlertGhost();
        }

    }

    private void CheckReach() // überprüft ob Verfolgung abgebrochen werden kann
    {
        if (GetPlayerDistance() > GetReach())
        {
            states.ReturnToIdle();
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
            states.BecomeAngry();
        }
    }

    private void Move() // überprüft ob aktuelles Ziel erreicht wurde und setzt ggf. neues Ziel
    {
        if (Util.GetDistance(transform.position, patrolpoints[currentpoint].position) < 0.2)
        {
            currentpoint++;
            if (currentpoint >= patrolpoints.Length)
            {
                currentpoint = 0;
            }
        }

        agent.SetDestination(patrolpoints[currentpoint].position);
    }

    private void Rotate()
    {
        Vector3 target = patrolpoints[currentpoint].position;
        float step = 2 * agent.speed * Time.deltaTime;

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
        Vector3 enemy = transform.position;
        Vector3 player = patrolpointsAlert[0].position;
        player = GameObject.Find("Player").transform.position;

        return Util.GetDistance(enemy, player);
    }

    // state listeners

    public void SetPatrolpointsIdle()
    {
        patrolpoints = patrolpointsIdle;
    }

    public void SetPatrolpointsAlert()
    {
        patrolpoints = patrolpointsAlert;
    }

    public void SetIdleSpeed()
    {
        agent.speed = idlespeed;
    }

    public void SetAlertSpeed()
    {
        agent.speed = alertspeed;
    }

    public void SetAngrySpeed()
    {
        agent.speed = angryspeed;
    }

    public void SetFrozen()
    {
        StartCoroutine(Freeze());
    }

    IEnumerator Freeze()
    {
        yield return new WaitForSeconds(freezetime);

        states.AlertGhost();
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
        states.OnAlert += SetPatrolpointsAlert;
        states.OnAlert += ResetCurrentpoint;
        states.OnAlert += SetAlertSpeed;
        states.OnAlert += SetHomePoint;

        states.OnBecomeAngry += SetAngrySpeed;

        states.OnFreeze += SetFrozen;

        states.OnReturnToIdle += SetPatrolpointsIdle;
        states.OnReturnToIdle += SetIdleSpeed;
    }
}
