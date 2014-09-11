using UnityEngine;
using System.Collections;

public class Power_Fallout : MonoBehaviour {
    
    public GameObject associatedEvent;
    public bool lightsOn;
    public int maxFlicker;

    public float minWaitVal;
    public float maxWaitVal;

    private int counter = 0;

	// Use this for initialization
	void Start () {
       associatedEvent.GetComponent<Tutorial_Controller>().OnPowerFalloff += OnPowerFalloff;
	}

    private void OnPowerFalloff()
    {
        {
            StartCoroutine(LightsOff());
        }
        audio.Play();
    }
	
	// Update is called once per frame
	void Update () {
        //if (!lightsOn)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetActive(lightsOn);
            }
        }
	
	}

    private IEnumerator LightsOff()
    {
        

        yield return new WaitForSeconds(getRandomWaitTime());

        lightsOn = !lightsOn;
        if (counter < maxFlicker)
        {
            counter++;
            StartCoroutine(LightsOff());
        }

       
    }

    private float getRandomWaitTime()
    {
        float randomOffset = Random.Range(minWaitVal, maxWaitVal);
        return randomOffset;
    }
}
