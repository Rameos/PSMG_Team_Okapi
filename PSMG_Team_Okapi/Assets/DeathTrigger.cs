﻿using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Player_Health playerHealth = GameObject.FindObjectOfType<Player_Health>();
        playerHealth.health = 0; 
    }


}
