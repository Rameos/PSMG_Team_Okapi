using UnityEngine;
using System.Collections;

public class Server_Panel_Blink_Controller : MonoBehaviour {
    public Material[] materials;
    public float intervalSec;
    public float minOffsetVal;
    public float maxOffsetVal;

    private Material actMat;
    private int indexCount = 0;

	// Use this for initialization
	void Start () {
        actMat = gameObject.renderer.material;

        StartCoroutine(ChangeTexture());
       
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator ChangeTexture()
    {
        while (true)
        {
            yield return new WaitForSeconds(getRandomWaitTime());

            if(indexCount < materials.Length-1)
            {
                indexCount++;
            }
            else
            {
                indexCount = 0;
            }
            actMat = materials[indexCount];
            gameObject.renderer.material = actMat; 

        }
    }

    private float getRandomWaitTime ()
    {
        float randomOffset = Random.Range(minOffsetVal, maxOffsetVal);
        return randomOffset;
    }
}
