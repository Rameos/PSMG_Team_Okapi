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
        CheckPoints();
        Transform();

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
        Debug.DrawRay(transform.position, patrolpoints[currentpoint].position, Color.red);
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

    private void CheckPoints()
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

    private void Transform()
    {
        if (Vector3.Angle(transform.forward, patrolpoints[currentpoint].position) > 5) // if not facing front
        {
            //transform.position = Vector3.RotateTowards(transform.forward, patrolpoints[currentpoint].position, currentspeed * Time.deltaTime, 0);
            transform.Rotate(transform.up * 10 * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolpoints[currentpoint].position, currentspeed * Time.deltaTime);
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
