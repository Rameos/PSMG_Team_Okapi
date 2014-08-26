using UnityEngine;
using System.Collections;

public class Enemy_AttackVisualFeedback : MonoBehaviour {

    public ParticleSystem[] particleSystems;

    private Enemy_PlayerAttacking playerAttacking;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Enemy_PlayerAttacking>().OnAttackingPlayer += OnAttackingPlayer;
	}

    private void OnAttackingPlayer()
    {
        foreach (ParticleSystem parSys in particleSystems) {
            parSys.Play();
        }
    }
}
