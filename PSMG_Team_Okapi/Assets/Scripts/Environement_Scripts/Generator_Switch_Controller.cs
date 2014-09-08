using UnityEngine;
using System.Collections;

public class Generator_Switch_Controller : MonoBehaviour
{

    public GameObject assoziatedGenerator;
    public Material lightOnMat;
    public Material lightOffMat;
    public bool isOn;

    private bool canPress = false;
    
    private GameObject player;
    private Animator anim;

    public delegate void GeneratorSwitchEventHandler(GameObject generator);
    public event GeneratorSwitchEventHandler OnActivateGeneratorSwitch;



    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canPress && !isOn)
            {
                isOn = true;
                anim.SetBool(Animator.StringToHash("isOn"), isOn);
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

              
                if(OnActivateGeneratorSwitch != null)
                {
                    // trigger Event
                    Debug.Log("Trigger");

                    OnActivateGeneratorSwitch(assoziatedGenerator);
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

