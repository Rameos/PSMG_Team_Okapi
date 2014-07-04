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
               
                Renderer button = gameObject.renderer;
                Material[] mats = button.materials;
                for (int i = 0; i < button.materials.Length; i++)
                {
                    Debug.Log(mats[i].name);
                    if (mats[i].name == "Light_Switch_Button_Light_Off (Instance)")
                    {
                        mats[i] = lightOnMat;
                        Debug.Log("switch Press");
                    }
                }
                button.materials = mats;

                if (OnActivateSwitch != null)
                {
                    // trigger Event
                    OnActivateSwitch(associatedLightsObject);
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

