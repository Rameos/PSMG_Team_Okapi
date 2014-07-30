using UnityEngine;
using System.Collections;

public class Terminal_Behaviour : MonoBehaviour {
    public Material onMaterial;
    public Material offMaterial;
    
    private bool alreadyPlayed;
    private bool canPlay;
    private GameObject player;
    private Material actMaterial;


	// Use this for initialization
	void Start () {
        alreadyPlayed = false;
        player = GameObject.FindGameObjectWithTag("Player");
        actMaterial = gameObject.renderer.material;
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(!audio.isPlaying)
        {
            actMaterial = offMaterial;
        }
        else
        {
            actMaterial = onMaterial;
        }
        gameObject.renderer.material = actMaterial;

        if (Input.GetMouseButtonDown(0))
        {
            if (canPlay)
            {
                playbackAudio();
            }
        }

	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == player)
        {
            canPlay = true;
            if (!alreadyPlayed)
            {
                playbackAudio();
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        canPlay = false;
    }

    private void visualPlaybackFeedback()
    {
        actMaterial = onMaterial;
        gameObject.renderer.material = actMaterial;
    }

    private void playbackAudio()
    {
        if (!audio.isPlaying)
        {
            audio.Play();
            visualPlaybackFeedback();
            alreadyPlayed = true;
        }
    }
}
