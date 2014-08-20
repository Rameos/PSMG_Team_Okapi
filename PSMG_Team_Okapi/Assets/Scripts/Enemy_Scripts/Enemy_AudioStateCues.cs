using UnityEngine;
using System.Collections;

public class Enemy_AudioStateCues : MonoBehaviour {

    public AudioClip alertCueClip;
    public float alertCueCooldownSec = 5;

    private bool alertCueReady = true;

    private Enemy_States enemyState;    

	// Use this for initialization
	void Start () {
        enemyState = gameObject.GetComponent<Enemy_States>();
        enemyState.OnAlert += PlayAlertCue;
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

    IEnumerator AlertCueCooldown()
    {
        alertCueReady = false;
        yield return new WaitForSeconds(alertCueCooldownSec);
        alertCueReady = true;
    }
}
