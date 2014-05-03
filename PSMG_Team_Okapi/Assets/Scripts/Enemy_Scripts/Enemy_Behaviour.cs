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

    private float maxStartDistance = 10; // maximale Entfernung zwischen Startpunkt und Geist bevor minPlayerReach erreicht wird
    public float maxPlayerDistance; // Obergrenze des Abstands für die Verfolgung
    public float minPlayerDistance; // Untergrenze des Abstands für die Verfolgung

    public float angryRadius; // Abstand zum Spieler, bei der Geist angry wird
    private float freezetime = 10; // wie lange geist schläft (in sec) wenn er eingefroren wird
    private float frozentime;

    private int currentpoint;
    private Vector3 homepoint;

    private enum States { idle, alert, angry, frozen };
    private States state;

    void Start()
    {
        patrolpoints = patrolpointsIdle;
        transform.position = patrolpoints[0].position;
        currentpoint = 0;
        currentspeed = movespeed;
        state = States.idle;
    }


    void Update()
    {

        Transform();

        switch (state)
        {
            case States.idle:
                CheckRadius();
                CheckEyeLine();
                break;
            case States.alert:
                CheckReach();
                CheckPlayerDist();
                break;
            case States.angry:
                CheckReach();
                break;
            case States.frozen:
                CheckFreeze();
                break;
        }
    }

    // for idle Ghost:

    private void CheckRadius()
    {
        if (GetPlayerDistance() < alertRadius) AlertGhost();
    }

    private void CheckEyeLine()
    {

    }

    private void AlertGhost()
    {
        state = States.alert;
        patrolpoints = patrolpointsAlert;
        currentpoint = 0;
        homepoint = transform.position;
        print("Ghost alert");

        // Geist soll gelb werden
    }

    // for alert or angry ghost

    private void CheckReach() // überprüft ob Verfolgung abgebrochen werden kann
    {
        if (GetPlayerDistance() > GetReach()) ReturnToIdle();
    }

    private float GetReach()
    {
        // berechnung vom Reach anhand von maxStartDistance, maxPlayerDistance, minPlayerDistance;

        float leeway = maxPlayerDistance - minPlayerDistance;
        float distance = GetDistance(homepoint, transform.position);
        float percentage = distance / maxStartDistance;

        return minPlayerDistance + leeway*percentage;
    }

    private void CheckPlayerDist() // überprüft ob wechsel zu Angry stattfinden soll
    {
        if (GetPlayerDistance() <= angryRadius) BecomeAngry();
    }

    private void ReturnToIdle()
    {
        state = States.idle;
        patrolpoints = patrolpointsIdle;
        print("Ghost idle");

        // Geist soll wieder grün werden
    }

    private void BecomeAngry()
    {
        state = States.angry;
        currentspeed = maxspeed;
        print("Ghost angry");

        // Geist soll rot werden
    }

    private void Freeze()
    {
        state = States.frozen;
        currentspeed = 0; // unnötig falls sleep verwendet wird
        print("Ghost frozen");

        // Geist soll blau werden

        frozentime = 0;
    }

    // for frozen Ghost

    private void CheckFreeze()
    {
        if (frozentime < freezetime) frozentime += Time.deltaTime;
        else Unfreeze();
    }

    private void Unfreeze()
    {
        state = States.alert;
        currentspeed = movespeed;
        print("Ghost alert");

        // Geist soll gelb werden
    }


    // for all states

    private void Transform()
    {
        if (transform.position == patrolpoints[currentpoint].position)
        {
            currentpoint++;
            if (currentpoint >= patrolpoints.Length) currentpoint = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, patrolpoints[currentpoint].position, currentspeed * Time.deltaTime);

    }

    private float GetPlayerDistance()
    {
        Vector3 enemy = transform.position;
        Vector3 player = patrolpointsAlert[0].position;

        return GetDistance(enemy, player);
    }

    private float GetDistance(Vector3 v1, Vector3 v2) // berechnet horizontalen abstand zwischen zwei vektoren
    {
        float a = Mathf.Abs(v1.x - v2.x);
        float b = Mathf.Abs(v1.z - v2.z);

        return Mathf.Sqrt(a * a + b * b);
    }
}
