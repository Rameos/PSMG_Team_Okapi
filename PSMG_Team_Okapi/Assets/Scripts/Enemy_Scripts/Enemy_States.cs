﻿using UnityEngine;
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

    public bool debug = false;

    void Start()
    {
        currentState = State.idle;
    }

    public void Idle()
    {
        currentState = State.idle;
        OnIdle();

        if (debug)
        {            
            print("Enemy idle");
        }
    }

    public void Alert()
    {
        currentState = State.alert;
        OnAlert();

        if (debug)
        {
            print("Enemy alert");
        }
    }

    public void Angry()
    {
        currentState = State.angry;
        OnAngry();

        if (debug)
        {            
            print("Enemy angry");
        }
    }

    public void Freeze()
    {
        currentState = State.frozen;
        OnFreeze();

        if (debug)
        {            
            print("Enemy frozen");
        }

    }
}
