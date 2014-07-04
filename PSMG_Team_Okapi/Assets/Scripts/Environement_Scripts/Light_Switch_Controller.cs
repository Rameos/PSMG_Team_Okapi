using UnityEngine;
using System.Collections;

public class Light_Switch_Controller : MonoBehaviour
{

    public GameObject associatedLightsObject;
    public Material lightOnMat;
    public Material lightOffMat;

    private bool canPress = false;
    private GameObject player;

    public delegate void LightSwitchEventHandler(GameObject lights);
    public event LightSwitchEventHandler OnActivateLightSwitch;



    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canPress)
            {
                foreach (Transform child in gameObject.transform)
                {
                    for (int i = 0; i < child.gameObject.renderer.materials.Length; i++)
                    {
                        Material[] mats = child.gameObject.renderer.materials;
                        if (child.gameObject.renderer.materials[i].name == "Light_Switch_Button_Light_Off (Instance)")
                        {
                            mats[i] = lightOnMat;
                        }
                        else if (child.gameObject.renderer.materials[i].name == "Light_Switch_Button_Light_On (Instance)" )
                        {
                            mats[i] = lightOffMat;
                        }
                        child.gameObject.renderer.materials = mats;

                    }
                }

                LightSwitchEventHandler handler = OnActivateLightSwitch;
                //if (handler != null)
                {
                    // trigger Event
                    Debug.Log("Trigger");

                    handler(associatedLightsObject);
                }
                audio.Play();
            }
        }
    }



    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.gameObject == player)
        {
            canPress = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        canPress = false;
    }
}

