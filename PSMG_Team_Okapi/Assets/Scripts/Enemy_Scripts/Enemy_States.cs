using UnityEngine;
using System.Collections;

public class Enemy_States : MonoBehaviour
{
    public delegate void StateChangeHandler();

    public event StateChangeHandler OnIdle = delegate { };
    public event StateChangeHandler OnAlert = delegate { };
    public event StateChangeHandler OnAngry = delegate { };
    public event StateChangeHandler OnFreeze = delegate { };    

    public enum State { idle, alert, angry, frozen };
    public State currentState;

    private bool debug = true;

    private Enemy_ChangeTexture changeTexture;


    void Start()
    {
        currentState = State.idle;
        changeTexture = gameObject.GetComponent<Enemy_ChangeTexture>();
    }

    public void Idle()
    {
        currentState = State.idle;
        OnIdle();

        if (debug)
        {
            changeTexture.changeLooks(State.idle);
            //print("Enemy idle");
        }
    }

    public void Alert()
    {
        currentState = State.alert;
        OnAlert();

        if (debug)
        {
            changeTexture.changeLooks(State.alert);
            //print("Enemy alert");
        }
    }

    public void Angry()
    {
        currentState = State.angry;
        OnAngry();

        if (debug)
        {
            changeTexture.changeLooks(State.angry);
            //print("Enemy angry");
        }
    }

    public void Freeze()
    {
        currentState = State.frozen;
        OnFreeze();

        if (debug)
        {
            changeTexture.changeLooks(State.frozen);
            //print("Enemy frozen");
        }

    }
}
