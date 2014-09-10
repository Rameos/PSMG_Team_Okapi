using UnityEngine;
using System.Collections;

public class TriggerAudioSourceController : MonoBehaviour {

    private GameObject player;
    private bool hasPlayed = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnTriggerEnter(Collider col)
    {
        if (!hasPlayed)
        {
            if (col.gameObject == player)
            {
                if (!audio.isPlaying)
                {
                    audio.Play();
                    hasPlayed = true;
                }
            }
        }
    }
}
