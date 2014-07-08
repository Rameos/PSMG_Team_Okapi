using UnityEngine;
using System.Collections;

public class HealthVisualFeedback : MonoBehaviour {

    public float alphaStage1 = 0.20f;
    public float alphaStage2 = 0.35f;
    public float alphaStage3 = 0.55f;

    public int healthThresholdStage1 = 90;
    public int healthThresholdStage2 = 50;
    public int healthThresholdStage3 = 15;

	// Use this for initialization
	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Player_Health health = player.GetComponent<Player_Health>();
        health.OnHealthChanged += OnHealthChanged;
	}

    void Awake()
    {
        guiTexture.pixelInset = new Rect(300f, 100f, Screen.width-600, Screen.height-200);
        guiTexture.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnHealthChanged(int newHealth, int oldHealth)
    {
        Color currentColor = guiTexture.color;

        if (newHealth <= 0)
        {
            currentColor.a = 0.0f;
        } 
        else if (newHealth <= healthThresholdStage3)
        {
            currentColor.a = alphaStage3;
        } 
        else if (newHealth <= healthThresholdStage2)
        {
            currentColor.a = alphaStage2;
        }
        else if (newHealth <= healthThresholdStage1)
        {
            currentColor.a = alphaStage1;
        }

        guiTexture.color = currentColor;
    }
}
