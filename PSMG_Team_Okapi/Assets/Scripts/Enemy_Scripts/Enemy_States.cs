using UnityEngine;
using System.Collections;

public class Enemy_States : MonoBehaviour
{
    public delegate void StateChangeHandler();

    public event StateChangeHandler OnAlert;
    public event StateChangeHandler OnBecomeAngry;
    public event StateChangeHandler OnFreeze;
    public event StateChangeHandler OnReturnToIdle;

    public enum States { idle, alert, angry, frozen };
    private States state;

    private bool debug = true;

    private float freezetime = 10; // zeit, die gegner eingefroren bleiben soll
    private float frozentime; // zeit, die gegner bereits eingefroren ist

    private Enemy_ChangeTexture changeTexture;


    void Start()
    {
        state = States.idle;
        changeTexture = gameObject.GetComponent<Enemy_ChangeTexture>();
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

        if (OnAlert != null)
        {
            OnAlert();
        }

        if (debug)
        {
            changeTexture.changeLooks("alert");
            //print("Enemy alert");
        }
    }

    public void ReturnToIdle()
    {
        state = States.idle;

        if (OnReturnToIdle != null)
        {
            OnReturnToIdle();
        }

        if (debug)
        {
            changeTexture.changeLooks("idle");
            //print("Enemy idle");
        }
    }

    public void BecomeAngry()
    {
        state = States.angry;

        if (OnBecomeAngry != null)
        {
            OnBecomeAngry();
        }

        if (debug)
        {
            changeTexture.changeLooks("angry");
            //print("Enemy angry");
        }
    }

    public void Freeze()
    {
        state = States.frozen;

        if (OnFreeze != null)
        {
            OnFreeze();
        }

        if (debug)
        {
            changeTexture.changeLooks("frozen");
            //print("Enemy frozen");
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
