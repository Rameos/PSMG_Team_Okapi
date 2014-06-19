using UnityEngine;
using System.Collections;

public class Enemy_ChangeTexture : MonoBehaviour 
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

    public void changeLooks(Enemy_States.State state)
    {
        Color oldColor = enemyBody.material.color;

        switch(state)
        {
            case Enemy_States.State.idle:
            {
                enemyBody.material = idleMat;
                break;
            }
            case Enemy_States.State.alert:
            {
                enemyBody.material = alertMat;
                break;
            }

            case Enemy_States.State.angry:
            {
                enemyBody.material = angryMat;
                break;
            }

            case Enemy_States.State.frozen:
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
