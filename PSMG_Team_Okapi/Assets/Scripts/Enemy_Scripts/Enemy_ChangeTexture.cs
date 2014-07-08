using UnityEngine;
using System.Collections;

public class Enemy_ChangeTexture : MonoBehaviour 
{
    public Material idleMat;
    public Material alertMat;
    public Material angryMat;
    public Material frozenMat;

    private Renderer enemyBody;
    private Enemy_States enemyState;

	// Use this for initialization
	void Start () 
    {
        enemyBody = gameObject.renderer;
        enemyState = gameObject.GetComponent<Enemy_States>();

        enemyState.OnIdle += OnIdle;
        enemyState.OnAlert += OnAlert;
        enemyState.OnAngry += OnAngry;
        enemyState.OnFreeze += OnFreeze;
	}

    private void OnIdle()
    {
        ChangeMaterial(idleMat);
    }

    private void OnAlert()
    {
        ChangeMaterial(alertMat);
    }

    private void OnAngry()
    {
        ChangeMaterial(angryMat);
    }

    private void OnFreeze()
    {
        ChangeMaterial(frozenMat);
    }

    private void ChangeMaterial(Material newMat)
    {
        Color oldColor = enemyBody.material.color;

        enemyBody.material = newMat;

        Color newColor = enemyBody.material.color;
        newColor.a = oldColor.a;
        enemyBody.material.color = newColor;
    }
    void OnCollisionEnter(Collision hit)
    {
        Debug.Log(hit);
    }    
}
