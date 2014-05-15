using UnityEngine;
using System.Collections;

public class Enemy_Behaviour : MonoBehaviour
{

    public Transform[] patrolpointsIdle; // reihe von waypoints
    public Transform[] patrolpointsAlert; // enthält erstmal nur Spieler
    private Transform[] patrolpoints;

    public float movespeed = 2; // geschwindigkeit wenn idle oder alert
    public float maxspeed = 4; // geschwindigkeit wenn angry
    private float currentspeed;

    public float alertRadius; // radius in dem spieler erkannt wird
    public float eyereach; // sichtweite des geistes (sollte gleich Sichtweite des Spielers sein)

    private float maxStartDistance = 20; // maximale Entfernung zwischen Startpunkt und Geist bevor minPlayerReach erreicht wird
    public float maxPlayerDistance = 15; // Obergrenze des Abstands für die Verfolgung
    public float minPlayerDistance = 11; // Untergrenze des Abstands für die Verfolgung

    public float angryRadius; // Abstand zum Spieler, bei der Geist angry wird

    private int currentpoint;
    private Vector3 homepoint;

    private Enemy_States states;

    void Start()
    {
        patrolpoints = patrolpointsIdle;
        transform.position = patrolpoints[0].position;
        currentpoint = 0;
        currentspeed = movespeed;

        states = gameObject.GetComponent<Enemy_States>();
    }

    void Update()
    {
        UpdateWayPoints();
        Move();

        switch (states.getState())
        {
            case Enemy_States.States.idle:
                CheckRadius();
                CheckEyeLine();
                break;
            case Enemy_States.States.alert:
                CheckReach();
                CheckPlayerDist();
                break;
            case Enemy_States.States.angry:
                CheckReach();
                break;
        }
    }

    private void CheckRadius()
    {
        if (GetPlayerDistance() < alertRadius)
        {
            states.AlertGhost();
        }
    }

    private void CheckEyeLine()
    {
        //public Transform target;

        /*NavMeshHit hit;
        if (NavMesh.Raycast(transform.position, patrolpoints[currentpoint].position, out hit, -1))
        {
            print("hit");
        }*/

        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = transform.forward;
        //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
    }

    private void CheckReach() // überprüft ob Verfolgung abgebrochen werden kann
    {
        if (GetPlayerDistance() > GetReach())
        {
            states.ReturnToIdle();
        }
    }

    private float GetReach()
    {
        // berechnung vom Reach anhand von maxStartDistance, maxPlayerDistance, minPlayerDistance;

        float leeway = maxPlayerDistance - minPlayerDistance;
        float distance = Util.GetDistance(homepoint, transform.position);
        float percentage = distance / maxStartDistance;

        return minPlayerDistance + leeway * percentage;
    }

    private void CheckPlayerDist() // überprüft ob wechsel zu Angry stattfinden soll
    {
        if (GetPlayerDistance() <= angryRadius)
        {
            states.BecomeAngry();
        }
    }

    private void UpdateWayPoints() // überprüft ob aktuelles Ziel erreicht wurde und setzt ggf. neues Ziel
    {
        if (Util.GetDistance(transform.position, patrolpoints[currentpoint].position) < 0.1)
        {
            currentpoint++;
            if (currentpoint >= patrolpoints.Length)
            {
                currentpoint = 0;
            }
        }
    }

    private void Move()
    {
        Vector3 target = patrolpoints[currentpoint].position;
        float step = currentspeed * Time.deltaTime;

		Vector3 v1 = new Vector3 (transform.forward.x, 0, transform.forward.z);
		Vector3 v2 = new Vector3 (target.x - transform.position.x, 0, target.z - transform.position.z);

        //print("Angle: " + Vector3.Angle(v1, v2));
        
        if (Vector3.Angle(v1, v2) > 2) // if not facing front
        { 
            Vector3 newDir = Vector3.RotateTowards(v1, v2, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir); // rotate to front
        }
        else
        {
            if (GetPlayerDistance() > 1.5)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, step);
            }
        }
    }

    private float GetPlayerDistance()
    {
        Vector3 enemy = transform.position;
        Vector3 player = patrolpointsAlert[0].position;

        return Util.GetDistance(enemy, player);
    }

    public void setPatrolpointsIdle()
    {
        patrolpoints = patrolpointsIdle;
    }

    public void setPatrolpointsAlert()
    {
        patrolpoints = patrolpointsAlert;
    }

    public void setMaxSpeed()
    {
        currentspeed = maxspeed;
    }

    public void resetSpeed()
    {
        currentspeed = movespeed;
    }

    public void setNoSpeed()
    {
        currentspeed = 0;
    }

    public void resetCurrentpoint()
    {
        currentpoint = 0;
    }

    public void setHomePoint()
    {
        homepoint = transform.position;
    }
}
