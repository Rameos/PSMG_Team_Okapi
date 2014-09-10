using UnityEngine;
using System.Collections;

public class TriggerAudioSourceController : MonoBehaviour {

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == player)
        {
            if(!audio.isPlaying)
            {
                audio.Play();
            }
        }
    }
}
