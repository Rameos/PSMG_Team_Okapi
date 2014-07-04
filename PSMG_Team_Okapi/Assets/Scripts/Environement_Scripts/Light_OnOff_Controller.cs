using UnityEngine;
using System.Collections;

public class Light_OnOff_Controller : MonoBehaviour {

   
    public GameObject associatedSwitch;
 

    private bool isOn;
    


    // Use this for initialization
    void Start()
    {
        if (associatedSwitch != null)
        {
            associatedSwitch.GetComponent<Light_Switch_Controller>().OnActivateSwitch += OnActivateSwitch;
        }
    }

    private void OnActivateSwitch(GameObject lights)
    {
        //if (lights == gameObject)
        {
            isOn = !isOn;
            Debug.Log("Licht: " + isOn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.SetActive(isOn);
        
    }
}
