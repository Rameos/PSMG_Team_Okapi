using UnityEngine;
using System.Collections;

public class Enemy_States : MonoBehaviour
{
    public delegate void StateChangeHandler();

    public event StateChangeHandler OnAlert = delegate{};
    public event StateChangeHandler OnBecomeAngry = delegate{};
    public event StateChangeHandler OnFreeze = delegate{};
    public event StateChangeHandler OnReturnToIdle = delegate{};

    public enum States { idle, alert, angry, frozen };
    private States state;

    private bool debug = true;

    private Enemy_ChangeTexture changeTexture;


    void Start()
    {
        state = States.idle;
        changeTexture = gameObject.GetComponent<Enemy_ChangeTexture>();
    }

    public void AlertGhost()
    {
        state = States.alert;
        OnAlert();

        if (debug)
        {
            changeTexture.changeLooks("alert");
            //print("Enemy alert");
        }
    }

    public void ReturnToIdle()
    {
        state = States.idle;
        OnReturnToIdle();

        if (debug)
        {
            changeTexture.changeLooks("idle");
            //print("Enemy idle");
        }
    }

    public void BecomeAngry()
    {
        state = States.angry;
        OnBecomeAngry();

        if (debug)
        {
            changeTexture.changeLooks("angry");
            //print("Enemy angry");
        }
    }

    public void Freeze()
    {
        state = States.frozen;
        OnFreeze();

        if (debug)
        {
            changeTexture.changeLooks("frozen");
            //print("Enemy frozen");
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
