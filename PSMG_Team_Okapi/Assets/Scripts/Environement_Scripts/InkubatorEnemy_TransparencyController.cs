using UnityEngine;
using System.Collections;

public class InkubatorEnemy_TransparencyController : MonoBehaviour {

    // negative == become invisible
    // positive == become visible
    public float currentDelta = 0.00f;
    public float becomeInvisibleDelta = -0.03f;
    public float becomeVisibleDelta = 0.005f;

    public float decaySec = 0.0f;

    public float transparencyMin = 0.0f;
    public float transparencyMax = 1.0f;

    private bool gazeActive = false;
    private float endOfDecay = 0;

    private InkubatorEnemy_GazeInteraction gazeInteraction;

	void Start () {
        currentDelta = becomeInvisibleDelta;
        //enemy = gameObject.transform.parent.gameObject;
        gazeInteraction = gameObject.GetComponentInChildren<InkubatorEnemy_GazeInteraction>();

        gazeInteraction.OnEnemyGazeEntered += GazeEnter;
        gazeInteraction.OnEnemyGazeExited += GazeExit;
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
        currentDelta = becomeVisibleDelta;
    }

    private void DecreaseVisibility()
    {
        currentDelta = becomeInvisibleDelta;
    }

    private void UpdateTransparency()
    {
        //Color c = gameObject.renderer.material.color;
        //c.a = Mathf.Min(Mathf.Max(c.a + currentDelta, transparencyMin), transparencyMax);
        //gameObject.renderer.material.color = c;

        UpdateTransparencyInChildren(gameObject.transform);
    }

    private void UpdateTransparencyInChildren(Transform parent)
    {
        for (int childIndex = 0; childIndex < parent.childCount; childIndex++)
        {
            Transform child = parent.GetChild(childIndex);
            Renderer childRenderer = child.gameObject.renderer;

            if (childRenderer != null)
            {
                foreach (Material childMat in childRenderer.materials)
                {
                    Color c = childMat.color;
                    c.a = Mathf.Min(Mathf.Max(c.a + currentDelta, transparencyMin), transparencyMax);
                    childMat.color = c;
                }
            }

            UpdateTransparencyInChildren(child);
        }
    }
}
