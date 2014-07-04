using UnityEngine;
using System.Collections;

public class Light_Switch_Controller : MonoBehaviour
{

    public GameObject associatedLightsObject;
    public Material lightOnMat;

    private bool canPress = false;
    private GameObject player;

    public delegate void StateChangeHandler(GameObject lights);
    public event StateChangeHandler OnActivateSwitch;



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
                        if (child.gameObject.renderer.materials[i].name == "Light_Switch_Button_Light_Off (Instance)")
                        {
                            Material[] mats = child.gameObject.renderer.materials;
                            mats[i] = lightOnMat;
                            child.gameObject.renderer.materials = mats;
                        }
                    }
                }
                

                if (OnActivateSwitch != null)
                {
                    // trigger Event
                    OnActivateSwitch(associatedLightsObject);
                    Debug.Log("Trigger");
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

