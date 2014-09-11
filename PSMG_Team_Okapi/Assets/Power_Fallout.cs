using UnityEngine;
using System.Collections;

public class Power_Fallout : MonoBehaviour {
    
    public GameObject associatedEvent;
    public bool lightsOn;

	// Use this for initialization
	void Start () {
       // associatedEvent.GetComponent<Tutorial_Controller>().OnActivateLightSwitch += OnPowerFalloff;
	}

    private void OnPowerFalloff()
    {
        lightsOn = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!lightsOn)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
	
	}
}
