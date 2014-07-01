using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomizeAmbientSounds : MonoBehaviour {

    public float minIntervalSec = 30.0f;
    public float maxIntervalSec = 120.0f;

    // used for monitoring only
    public float intervalSec = 0.0f;
    public AudioClip lastClip;
    public float lastClipVolume;

    // specify clips and corresponding volume in unity
    public AudioClip[] audioClips;
    public float[] volumes;
    
    	
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
    private void SetupRandomClip()
    {
        int index = Random.Range(0, audioClips.Length);

        lastClip = audioClips[index];
        lastClipVolume = index < volumes.Length ? volumes[index] : 100;
    }

    IEnumerator PlayRandomSounds()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervalSec);

            Vector3 soundLocation = gameObject.transform.position;
            SetupRandomClip();

            AudioSource.PlayClipAtPoint(lastClip, soundLocation, lastClipVolume);

            GenerateRandomInterval();
        }
    }
}
