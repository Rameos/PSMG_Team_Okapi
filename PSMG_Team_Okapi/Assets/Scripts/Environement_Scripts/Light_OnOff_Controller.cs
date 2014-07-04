using UnityEngine;
using System.Collections;

public class Light_OnOff_Controller : MonoBehaviour {

   
    public GameObject associatedSwitch;
    public bool isOn = true;
    


    // Use this for initialization
    void Start()
    {
        if (associatedSwitch != null)
        {
            associatedSwitch.GetComponent<Light_Switch_Controller>().OnActivateLightSwitch += OnActivateSwitch;
        }
    }

    private void OnActivateSwitch(GameObject lights)
    {
        if (lights == gameObject)
        {
            isOn = !isOn;
            Debug.Log("Licht: " + isOn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(isOn);
            //gameObject.SetActive(isOn);
        }
        
    }
}
