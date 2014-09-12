using UnityEngine;
using System.Collections;

public class EndOfGameMessageStart : MonoBehaviour {

    public GameObject associatedSwitch;

	// Use this for initialization
	void Start () {
        if (associatedSwitch != null)
        {
            associatedSwitch.GetComponent<Generator_Switch_Controller>().OnActivateGeneratorSwitch += OnActivateSwitch;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnActivateSwitch(GameObject obj)

    {
       // Debug.Log("TEXT");
       // if (obj == associatedSwitch)
        
            audio.Play();
        
    }
}
