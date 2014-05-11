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

    private ChangeTexture changeTexture;
    private bool debug = true;

    void Start()
    {
        patrolpoints = patrolpointsIdle;
        transform.position = patrolpoints[0].position;
        currentpoint = 0;
        currentspeed = movespeed;
        state = States.idle;

        changeTexture = gameObject.GetComponent<ChangeTexture>();
        Debug.Log(changeTexture);
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
        if (GetPlayerDistance() < alertRadius)
        {
            AlertGhost();
        }
    }

    private void CheckEyeLine()
    {

    }

    private void AlertGhost()
    {
        state = States.alert;
        patrolpoints = patrolpointsAlert;
        currentpoint = 0;
        currentspeed = movespeed;
        homepoint = transform.position;

        if (debug) // Geist soll gelb werden
        {
            changeTexture.changeLooks("alert");
            print("Ghost alert");
        }
    }

    // for alert or angry ghost

    private void CheckReach() // überprüft ob Verfolgung abgebrochen werden kann
    {
        if (GetPlayerDistance() > GetReach())
        {
            ReturnToIdle();
        }
    }

    private float GetReach()
    {
        // berechnung vom Reach anhand von maxStartDistance, maxPlayerDistance, minPlayerDistance;

        float leeway = maxPlayerDistance - minPlayerDistance;
        float distance = Util.GetDistance(homepoint, transform.position);
        float percentage = distance / maxStartDistance;

        return minPlayerDistance + leeway*percentage;
    }

    private void CheckPlayerDist() // überprüft ob wechsel zu Angry stattfinden soll
    {
        if (GetPlayerDistance() <= angryRadius)
        {
            BecomeAngry();
        }
    }

    private void ReturnToIdle()
    {
        state = States.idle;
        patrolpoints = patrolpointsIdle;
        currentspeed = movespeed;

        if (debug) // Geist soll wieder grün werden
        {
            changeTexture.changeLooks("idle");
            print("Ghost idle");
        }
    }

    private void BecomeAngry()
    {
        state = States.angry;
        currentspeed = maxspeed;

        if (debug) // Geist soll rot werden
        {
            changeTexture.changeLooks("angry");
            print("Ghost angry");
        }
    }

    private void Freeze()
    {
        state = States.frozen;
        currentspeed = 0;

        // Geist soll blau werden
        if(debug)
        {
            changeTexture.changeLooks("frozen");
            print("Ghost frozen");
        }

        frozentime = 0;
    }

    // for frozen Ghost

    private void CheckFreeze()
    {
        if (frozentime < freezetime)
        {
            frozentime += Time.deltaTime;
        }
        else
        {
            AlertGhost();
        }
    }


    // for all states

    private void Transform()
    {
        if(Util.GetDistance(transform.position,patrolpoints[currentpoint].position) < 0.1)
        {
            currentpoint++;
            if (currentpoint >= patrolpoints.Length)
            {
                currentpoint = 0;
            }
        }
        if (false) // if not facing front
        {
            Turn();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolpoints[currentpoint].position, currentspeed * Time.deltaTime);
        }
    }

    private void Turn()
    {
        // while not facing front, rotate.
    }

    private float GetPlayerDistance()
    {
        Vector3 enemy = transform.position;
        Vector3 player = patrolpointsAlert[0].position;

        return Util.GetDistance(enemy, player);
    }
}
