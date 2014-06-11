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

    private bool gazeActive = false;
    private float endOfDecay = 0;

    private Enemy_States status;
    private Enemy_GazeInteraction gazeInteraction;

	// Use this for initialization
	void Start () {
        currentDelta = idleBecomeInvisible;
        //enemy = gameObject.transform.parent.gameObject;
        status = gameObject.GetComponent<Enemy_States>();
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
        switch (status.getState())
        {
            case Enemy_States.States.idle:
                currentDelta = idleBecomeVisible;
                break;
            case Enemy_States.States.alert:
                currentDelta = alertBecomeVisible;
                break;
            case Enemy_States.States.angry:
                currentDelta = angryBecomeVisible;
                break;
            case Enemy_States.States.frozen:
            // doesn't matter: no transparent shader
            default:
                currentDelta = idleBecomeVisible;
                break;
        }
    }

    private void DecreaseVisibility()
    {
        switch (status.getState())
        {
            case Enemy_States.States.idle:
                currentDelta = idleBecomeInvisible;
                break;
            case Enemy_States.States.alert:
                currentDelta = alertBecomeInvisible;
                break;
            case Enemy_States.States.angry:
                currentDelta = angryBecomeInvisible;
                break;
            case Enemy_States.States.frozen:
            // doesn't matter: no transparent shader
            default:
                currentDelta = idleBecomeInvisible;
                break;
        }
    }

    private void UpdateTransparency()
    {
        Color c = gameObject.renderer.material.color;
        c.a = Mathf.Min(Mathf.Max(c.a + currentDelta, 0.0f), 1.0f);
        gameObject.renderer.material.color = c;        

        for (int childIndex = 0; childIndex < gameObject.transform.childCount; childIndex++)
        {
            Transform child = gameObject.transform.GetChild(childIndex);
            Renderer childRenderer = child.gameObject.renderer;

            if (childRenderer != null)
            {
                Color childColor = childRenderer.material.color;
                childColor.a = c.a;

                childRenderer.material.color = childColor;
            }
        }
    }
}
