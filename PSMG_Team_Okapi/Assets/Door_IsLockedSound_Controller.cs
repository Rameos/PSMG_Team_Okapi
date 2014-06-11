using UnityEngine;
using System.Collections;

public class Door_IsLockedSound_Controller : MonoBehaviour {
    
    private GameObject associatedSwitch;
    private GameObject player;
    private bool isLocked;
	// Use this for initialization
	void Start () {
        associatedSwitch = GetComponentInParent<Door_Animation>().associatedSwitch;
        associatedSwitch.GetComponent<SwitchUnit_Controller>().OnActivateSwitch += OnActivateSwitch;
        isLocked = GetComponentInParent<Door_Animation>().isLocked;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	

    void OnActivateSwitch(GameObject door)
    {
        isLocked = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == player)
            if (isLocked)
                audio.Play();
    }
}
