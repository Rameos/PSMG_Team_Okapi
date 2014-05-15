﻿using UnityEngine;
using System.Collections;

public class ChangeTexture : MonoBehaviour 
{
    public Material idleMat;
    public Material alertMat;
    public Material angryMat;
    public Material frozenMat;

    private Renderer enemyBody;

	// Use this for initialization
	void Start () 
    {
        enemyBody = gameObject.renderer;
	}

    public void changeLooks(string state)
    {
        Color oldColor = enemyBody.material.color;

        switch(state)
        {
            case "idle":
            {
                enemyBody.material = idleMat;
                break;
            }
            case "alert":
            {
                enemyBody.material = alertMat;
                break;
            }
                
            case "angry":
            {
                enemyBody.material = angryMat;
                break;
            }

            case "frozen":
            {
                enemyBody.material = frozenMat;
                break;
            }
                
            default:
                break;
        }

        Color newColor = enemyBody.material.color;
        newColor.a = oldColor.a;
        enemyBody.material.color = newColor; 
    }
    void OnCollisionEnter(Collision hit)
    {
        Debug.Log(hit);
    }

    
}