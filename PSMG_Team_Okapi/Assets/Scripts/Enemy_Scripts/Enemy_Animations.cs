using UnityEngine;
using System.Collections;

public class Enemy_Animations : MonoBehaviour
{

    private Animator anim;

    private Enemy_States enemyState;
    private int idle = 0;
    private int alert = 1;
    private int angry = 2;
    private int frozen = 3;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        enemyState = gameObject.GetComponent<Enemy_States>();
        enemyState.OnIdle += OnIdle;
        enemyState.OnAlert += OnAlert;
        enemyState.OnAngry += OnAngry;
        enemyState.OnFreeze += OnFreeze;
    }

    private void OnIdle()
    {
        setAnimVar(idle);
    }

    private void OnAlert()
    {
        setAnimVar(alert);
    }

    private void OnAngry()
    {
        setAnimVar(angry);
    }

    private void OnFreeze()
    {
        setAnimVar(frozen);
    }


    private void setAnimVar(int val)
    {
        anim.SetInteger(Animator.StringToHash("state"), val);
    }
}
   
   
