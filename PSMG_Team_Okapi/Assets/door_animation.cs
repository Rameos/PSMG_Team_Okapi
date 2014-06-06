using UnityEngine;
using System.Collections;

public class door_animation : MonoBehaviour
{

    public bool isOpen;
    public bool isLocked;
    

    public Light[] doorStatusLight;

    public Color neutralLight;
    public Color lockedLight;
    public Color unlockedLight;

    private Animator anim;
    private GameObject player;
    private Renderer renderer;
    private bool showStatus = false;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

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
        //play Sound
    }

    void OnTriggerExit(Collider other)
    {
        isOpen = false;
        showStatus = false;
    }
}
