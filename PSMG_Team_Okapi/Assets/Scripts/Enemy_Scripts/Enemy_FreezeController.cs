using UnityEngine;
using System.Collections;

public class Enemy_FreezeController : MonoBehaviour {

    public float freezingAmount = 0;
    public float freezingAmountPerSec = 50;
    public float thawingTime = 5;    

    public delegate void FreezeHandler();

    public event FreezeHandler OnStartFreezing = delegate { };
    public event FreezeHandler OnStopFreezing = delegate { };
    public event FreezeHandler OnFrozen = delegate { };
    public event FreezeHandler OnThawed = delegate { };

    private Enemy_States enemyState;
    private Enemy_GazeInteraction gazeInteraction;

    private float FreezingStart = 0;
    private bool isCurrentlyFreezing = false;

	void Start () {
        enemyState = gameObject.GetComponent<Enemy_States>();
        
        gazeInteraction = gameObject.GetComponentInChildren<Enemy_GazeInteraction>();
        gazeInteraction.OnEnemyGazeEntered += OnGazeEntered;
        gazeInteraction.OnEnemyGazeExited += OnGazeExited;
	}

    void Update()
    {
        if (isCurrentlyFreezing)
        {
            freezingAmount = Mathf.Min(100, freezingAmount + (freezingAmountPerSec * Time.deltaTime));

            if (freezingAmount >= 100)
            {
                enemyState.Freeze();
                OnFrozen();

                StartCoroutine("Thawing");

                Debug.Log("frozen");
            }
        }
        else
        {
            freezingAmount = Mathf.Max(0, freezingAmount - freezingAmountPerSec * 0.25f * Time.fixedDeltaTime);
        }

        //Debug.Log("print: " + freezingAmount);
    }

    private void OnGazeEntered()
    {
        bool isAlert = enemyState.currentState == Enemy_States.State.alert;
        bool isAngry = enemyState.currentState == Enemy_States.State.angry;

        if (isAlert || isAngry)
        {
            //Debug.Log("gaze entered: " + enemyState.currentState);
            isCurrentlyFreezing = true;
            OnStartFreezing();
        }        
    }

    private void OnGazeExited()
    {
        isCurrentlyFreezing = false;
        OnStopFreezing();
    }

    IEnumerator Thawing()
    {
        yield return new WaitForSeconds(thawingTime);

        OnThawed();
        enemyState.Alert();
    }
}
