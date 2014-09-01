using UnityEngine;
using System.Collections;

public class Door_Animation : MonoBehaviour
{

    
    public bool isLocked;
    
    public Light[] doorStatusLight;
    public GameObject associatedSwitch;

    public Color neutralLight;
    public Color lockedLight;
    public Color unlockedLight;

    private AudioSource[] audioSources;

    private bool isOpen;
    private Animator anim;
    private GameObject player;    
    private bool showStatus = false;



    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (associatedSwitch != null)
        {
            associatedSwitch.GetComponent<SwitchUnit_Controller>().OnActivateSwitch += OnActivateSwitch;
        }        

        audioSources = GetComponents<AudioSource>();

    }

    private void OnActivateSwitch(GameObject[] doors)
    {
        Debug.Log("door unlocked");
        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i] == gameObject)
            {
                isLocked = false;
               
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool(Animator.StringToHash("open"), isOpen);
        if (!showStatus)
        {
            for (int i = 0; i < doorStatusLight.Length; i++)
            {
                doorStatusLight[i].color = Color.Lerp(doorStatusLight[i].color, neutralLight, Time.deltaTime * 0.5f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        showStatus = true;
        if (other.gameObject == player)
        {
            //statusLights();
            if (!isLocked)
            {
                isOpen = true;
                if (!audio.isPlaying)
                    audioSources[0].Play();
            }
            else
            {
                playLockedSound();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            statusLights();
        }
    }



    private void statusLights()
    {
        for (int i = 0; i < doorStatusLight.Length; i++)
        {
            if (isLocked)
            {
                doorStatusLight[i].color = Color.Lerp(doorStatusLight[i].color,lockedLight, Time.deltaTime * 5);
            }
            else
            {
                doorStatusLight[i].color = Color.Lerp(doorStatusLight[i].color, unlockedLight, Time.deltaTime * 5);
            }
        }
    }

    void playLockedSound()
    {
        audioSources[2].Play();
    }

    void OnTriggerExit(Collider other)
    {
        if (isOpen)
            audioSources[1].Play();
        isOpen = false;
        showStatus = false;
        
    }
}
