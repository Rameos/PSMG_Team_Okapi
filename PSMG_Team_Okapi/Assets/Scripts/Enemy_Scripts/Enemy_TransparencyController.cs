using UnityEngine;
using System.Collections;

public class Enemy_TransparencyController : MonoBehaviour {

    // negative == become invisible
    // positive == become visible
    public float currentDelta = 0.00f;
    public float idleBecomeInvisible = -0.03f;
    public float idleBecomeVisible = 0.005f;
    public float alertBecomeInvisible = -0.01f;
    public float alertBecomeVisible = 0.05f;
    public float angryBecomeInvisible = -0.0005f;
    public float angryBecomeVisible = 0.1f;

    public float decaySec = 0.0f;

    public float transparencyMin = 0.0f;
    public float transparencyMax = 1.0f;

    private bool gazeActive = false;
    private float endOfDecay = 0;

    private Enemy_States enemyState;
    private Enemy_GazeInteraction gazeInteraction;

	// Use this for initialization
	void Start () {
        currentDelta = idleBecomeInvisible;
        //enemy = gameObject.transform.parent.gameObject;
        enemyState = gameObject.GetComponent<Enemy_States>();
        gazeInteraction = gameObject.GetComponentInChildren<Enemy_GazeInteraction>();

        gazeInteraction.OnEnemyGazeEntered += GazeEnter;
        gazeInteraction.OnEnemyGazeExited += GazeExit;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate()
    {
        UpdateRates();
        UpdateTransparency();
    }

    private void UpdateRates()
    {
        float currentTime = Time.fixedTime;        

        if (gazeActive || endOfDecay >= currentTime)
        {
            IncreaseVisibility();
        }
        else
        {
            DecreaseVisibility();
        }              
    }

    private void GazeEnter()
    {
        gazeActive = true;
        
        float currentTime = Time.fixedTime;
        // reset current decay when gaze enters
        endOfDecay = currentTime + decaySec;        
    }

    private void GazeExit()
    {
        gazeActive = false;
    }

    private void IncreaseVisibility()
    {
        switch (enemyState.currentState)
        {
            case Enemy_States.State.idle:
                currentDelta = idleBecomeVisible;
                break;
            case Enemy_States.State.alert:
                currentDelta = alertBecomeVisible;
                break;
            case Enemy_States.State.angry:
                currentDelta = angryBecomeVisible;
                break;
            case Enemy_States.State.frozen:
            // doesn't matter: no transparent shader
            default:
                currentDelta = idleBecomeVisible;
                break;
        }
    }

    private void DecreaseVisibility()
    {
        switch (enemyState.currentState)
        {
            case Enemy_States.State.idle:
                currentDelta = idleBecomeInvisible;
                break;
            case Enemy_States.State.alert:
                currentDelta = alertBecomeInvisible;
                break;
            case Enemy_States.State.angry:
                currentDelta = angryBecomeInvisible;
                break;
            case Enemy_States.State.frozen:
            // doesn't matter: no transparent shader
            default:
                currentDelta = idleBecomeInvisible;
                break;
        }
    }

    private void UpdateTransparency()
    {
        Color c = gameObject.renderer.material.color;
        c.a = Mathf.Min(Mathf.Max(c.a + currentDelta, transparencyMin), transparencyMax);
        gameObject.renderer.material.color = c;        

        for (int childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
        {
            Transform child = gameObject.transform.GetChild(childIndex);
            if (child.gameObject.GetComponent("ParticleSystem") == null)
            {
                Renderer childRenderer = child.gameObject.renderer;

                if (childRenderer != null)
                {
                    foreach (Material childMat in childRenderer.materials)
                    {
                        Color childColor = childMat.color;
                        childColor.a = c.a;

                        childMat.color = childColor;
                    }
                }
            }
        }
    }
}
