using UnityEngine;
using System.Collections;

public class Enemy_FreezeController : MonoBehaviour {

    public float freezingAmount = 0.0f;
    public float freezingAmountPerSec = 30.0f;
    public float thawingTime = 5.0f;    

    public delegate void FreezeHandler();

    public event FreezeHandler OnStartFreezing = delegate { };
    public event FreezeHandler OnStopFreezing = delegate { };
    public event FreezeHandler OnFrozen = delegate { };
    public event FreezeHandler OnThawed = delegate { };

    public bool debug = false;

    private Enemy_States enemyState;
    private Enemy_GazeInteraction gazeInteraction;
    
    private bool isCurrentlyFreezing = false;

    // make these public for debugging in unity
    private bool isAlert= false;
    private bool isAngry = false;

	void Start () {
        enemyState = gameObject.GetComponent<Enemy_States>();
        
        gazeInteraction = gameObject.GetComponentInChildren<Enemy_GazeInteraction>();
        gazeInteraction.OnEnemyGazeStay += OnGazeStay;
        gazeInteraction.OnEnemyGazeExited += OnGazeExited;
	}

    void FixedUpdate()
    {
        if (isCurrentlyFreezing)
        {
            freezingAmount = freezingAmount + (freezingAmountPerSec * Time.fixedDeltaTime);
            freezingAmount = Mathf.Min(100.0f, freezingAmount);

            if (freezingAmount >= 100)
            {
                enemyState.Freeze();
                OnFrozen();
                isCurrentlyFreezing = false;
                freezingAmount = 0.0f;

                StartCoroutine("Thawing");

                Debug.Log("frozen");
            }
        }
        else
        {
            freezingAmount = Mathf.Max(0, freezingAmount - (freezingAmountPerSec * 0.25f * Time.fixedDeltaTime));
        }

        if (debug)
        {
            //Debug.Log("print: " + freezingAmount);
        }        
    }

    private void OnGazeStay()
    {
        isAlert = enemyState.currentState == Enemy_States.State.alert;
        isAngry = enemyState.currentState == Enemy_States.State.angry;

        if (!isCurrentlyFreezing && (isAlert || isAngry))
        {
            if (debug)
            {
                Debug.Log("gaze staying: " + enemyState.currentState);
            }
            
            isCurrentlyFreezing = true;
            OnStartFreezing();
        }        
    }

    private void OnGazeExited()
    {
        if (isCurrentlyFreezing)
        {
            isCurrentlyFreezing = false;
            OnStopFreezing();
            
            if (debug)
            {
                Debug.Log("gaze exited");
            }
        }
    }

    IEnumerator Thawing()
    {
        yield return new WaitForSeconds(thawingTime);

        OnThawed();
        enemyState.Alert();

        if (debug)
        {
            Debug.Log("thawed");
        }
    }
}
