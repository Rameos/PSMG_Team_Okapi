using UnityEngine;
using System.Collections;

public class Player_GruntController : MonoBehaviour {

    // used for monitoring only
    public AudioClip lastClip;
    public float lastClipVolume;

    // specify clips and corresponding volume in unity
    public AudioClip[] audioClips;
    public float[] volumes;

	void Start () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Player_Health health = player.GetComponent<Player_Health>();
        health.OnHealthChanged += OnHealthChanged;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnHealthChanged(int newHealth, int oldHealth)
    {
        if(newHealth < oldHealth)
        {
            PlayRandomSound();
        }
    }

    private void ChooseRandomClip()
    {
        int index = Random.Range(0, audioClips.Length);

        lastClip = audioClips[index];
        lastClipVolume = index < volumes.Length ? volumes[index] : 100;
    }

    private void PlayRandomSound()
    {
            Vector3 soundLocation = gameObject.transform.position;
            ChooseRandomClip();
            AudioSource.PlayClipAtPoint(lastClip, soundLocation, lastClipVolume);
            
    }
}
