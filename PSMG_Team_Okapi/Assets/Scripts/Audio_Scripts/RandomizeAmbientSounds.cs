using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomizeAmbientSounds : MonoBehaviour {

    public float minIntervalSec = 30.0f;
    public float maxIntervalSec = 120.0f;

    public float intervalSec = 0.0f;

    public AudioClip[] audioClips;    
    	
	void Start () {
        // init interval
        if (intervalSec <= 0)
        {
            GenerateRandomInterval();
        }

        StartCoroutine(PlayRandomSounds());
	}
	
    private void GenerateRandomInterval()
    {
        intervalSec = Random.Range(minIntervalSec, maxIntervalSec);

        //intervalSec = 4.0f; // chosen by a fair dice roll
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalSec);

            Vector3 soundLocation = gameObject.transform.position;
            AudioClip clip = audioClips[Random.Range(0, audioClips.Length)];

            AudioSource.PlayClipAtPoint(clip, soundLocation);

            GenerateRandomInterval();
        }
    }
}
