using UnityEngine;
using System.Collections;

public class Enemy_AttackVisualFeedback : MonoBehaviour {

    public ParticleSystem[] particleSystems;
    private GameObject light;
    private LensFlare flare;
    public float speed;
    public float lightBrightness;
    public float lightReach;
    public float flareBrightness;

    private float minimum = 0;

    private Enemy_PlayerAttacking playerAttacking;

	// Use this for initialization
	void Start () {
        Transform mychildtransform = transform.FindChild("Point light");
        light = mychildtransform.gameObject;

        flare = light.GetComponent<LensFlare>();
        gameObject.GetComponent<Enemy_PlayerAttacking>().OnAttackingPlayer += OnAttackingPlayer;
        
	}

    private void OnAttackingPlayer()
    {
        foreach (ParticleSystem parSys in particleSystems) {
            parSys.Play();
        }
        light.light.intensity = lightBrightness;
        light.light.range = lightReach;
        flare.brightness = flareBrightness;
    }

    void Update()
    {
        if (light.light.intensity > 0)
        {
            light.light.intensity = Mathf.Lerp(light.light.intensity, minimum, Time.deltaTime * speed);
            light.light.range = Mathf.Lerp(light.light.range, minimum, Time.deltaTime * speed);
            flare.brightness = Mathf.Lerp(flare.brightness, minimum, Time.deltaTime * speed);
        }
    }

}
