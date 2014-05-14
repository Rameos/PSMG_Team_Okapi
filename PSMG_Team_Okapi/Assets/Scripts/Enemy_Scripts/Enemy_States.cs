using UnityEngine;
using System.Collections;

public class Enemy_States : MonoBehaviour
{

    public enum States { idle, alert, angry, frozen };
    private static States state;

    private static bool debug = true;

    private float freezetime = 10; // wie lange geist schläft (in sec) wenn er eingefroren wird
    private static float frozentime;

    private static ChangeTexture changeTexture;
    private static Enemy_Behaviour behaviour;


    void Start()
    {
        state = States.idle;
        changeTexture = gameObject.GetComponent<ChangeTexture>();
        behaviour = gameObject.GetComponent<Enemy_Behaviour>();
        Debug.Log(changeTexture);
    }

    void Update()
    {
        if (state == States.frozen)
        {
            CheckFreeze();
        }
    }

    public void AlertGhost()
    {
        state = States.alert;
        behaviour.setPatrolpointsAlert();
        behaviour.resetCurrentpoint();
        behaviour.resetSpeed();
        behaviour.setHomePoint();

        if (debug) // Geist soll gelb werden
        {
            changeTexture.changeLooks("alert");
            print("Ghost alert");
        }
    }

    public void ReturnToIdle()
    {
        state = States.idle;
        behaviour.setPatrolpointsIdle();
        behaviour.resetSpeed();

        if (debug) // Geist soll wieder grün werden
        {
            changeTexture.changeLooks("idle");
            print("Ghost idle");
        }
    }

    public void BecomeAngry()
    {
        state = States.angry;
        behaviour.setMaxSpeed();

        if (debug) // Geist soll rot werden
        {
            changeTexture.changeLooks("angry");
            print("Ghost angry");
        }
    }

    public void Freeze()
    {
        state = States.frozen;
        behaviour.setNoSpeed();

        // Geist soll blau werden
        if (debug)
        {
            changeTexture.changeLooks("frozen");
            print("Ghost frozen");
        }

        frozentime = 0;
    }

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

    public States getState()
    {
        return state;
    }

    public void setState(States newState)
    {
        state = newState;
    }
}
