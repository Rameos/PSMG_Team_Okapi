using UnityEngine;
using System.Collections;

public class Enemy_AudioStateCues : MonoBehaviour {

    public AudioClip alertCueClip;
    public AudioClip angryCueClip;
    public AudioClip frozenCueClip;
    public float alertCueCooldownSec = 5;

    private bool alertCueReady = true;
    private bool angryCueReady = true;
    private bool frozenCueReady = true;

    private Enemy_States enemyState;    

	// Use this for initialization
	void Start () {
        enemyState = gameObject.GetComponent<Enemy_States>();
        enemyState.OnAlert += PlayAlertCue;
        enemyState.OnAngry += PlayAngryCue;
        enemyState.OnFreeze += PlayFrozenCue;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void PlayAlertCue()
    {
        if (alertCueClip != null && alertCueReady)
        {
            StartCoroutine(AlertCueCooldown());
            AudioSource.PlayClipAtPoint(alertCueClip, transform.position, 100.0f);                        
        }
    }

    private void PlayAngryCue()
    {
        if (angryCueClip != null && angryCueReady)
        {
            AudioSource.PlayClipAtPoint(angryCueClip, transform.position, 100.0f);
        }
    }

    private void PlayFrozenCue()
    {
        if(frozenCueClip != null && frozenCueReady)
        {
            AudioSource.PlayClipAtPoint(frozenCueClip, transform.position, 100.0f);
        }
    }

    IEnumerator AlertCueCooldown()
    {
        alertCueReady = false;
        yield return new WaitForSeconds(alertCueCooldownSec);
        alertCueReady = true;
    }
}
