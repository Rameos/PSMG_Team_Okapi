using UnityEngine;
using System.Collections;

public class SwitchUnit_Controller : MonoBehaviour {

    public GameObject associatedDoor;
    public Material unlockedMat;

    private bool canPress = false;
    private GameObject player;

    public delegate void StateChangeHandler(GameObject door);
    public event StateChangeHandler OnActivateSwitch;



	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player"); 
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if(canPress)
            {
                Renderer statusScreen = gameObject.renderer;
                Material[] mats = statusScreen.materials;
                for (int i = 0; i < statusScreen.materials.Length; i++)
                {
                    if (mats[i].name == "Switch_Main_Screen (Instance)")
                    {
                        mats[i] = unlockedMat;
                    }
                }
                statusScreen.materials = mats;    
                
                if (OnActivateSwitch != null)
                {
                    // trigger Event
                    OnActivateSwitch(associatedDoor);
                }
                audio.Play();
            }
        }
	}

   

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if(other.gameObject == player)
        {
            canPress = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        canPress = false;
    }
}
