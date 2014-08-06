using UnityEngine;
using System.Collections;

public class Player_HealthAudioController : MonoBehaviour
{

    // used for monitoring only
    public AudioClip lastClip;
    public float lastClipVolume;

    // specify clips and corresponding volume in unity
    public AudioClip[] audioClips;
    public float[] volumes;

    // specify thresholds and corresponding time intervals

    public int[] healthThresholdStages;
    public float[] intervals; // in secs

    private float currentHealth;
    private Player_Health playerHealth;

    private bool playingSound = false;
    private int currentThreshold = 0;
    private float currentInterval = 0;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<Player_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = playerHealth.health;
        updateThreshold();
    }

    private void updateThreshold()
    {
        if (currentHealth > healthThresholdStages[0])
        {
            if (currentThreshold != 0)
            {
                currentThreshold = 0;
                StopCoroutine("PlayRandomSounds");
                playingSound = false;
            }
        }
        else
        {
            for (int i = healthThresholdStages.Length; i > 0; i--)
            {
                if (currentHealth <= healthThresholdStages[i - 1])
                {
                    currentThreshold = i;
                    currentInterval = intervals[i - 1];
                    break;
                }
            }
        }

        // überprüft ob Coroutine neu gestartet werden muss

        if (currentThreshold != 0 && playingSound == false)
        {
            StartCoroutine("PlayRandomSounds");
        }
    }

    private void ChooseRandomClip()
    {
        int index = Random.Range(0, audioClips.Length);

        lastClip = audioClips[index];
        lastClipVolume = index < volumes.Length ? volumes[index] : 100;
    }

    IEnumerator PlayRandomSounds()
    {
        playingSound = true;

        while (true)
        {
            yield return new WaitForSeconds(currentInterval);

            Vector3 soundLocation = gameObject.transform.position;
            ChooseRandomClip();
            AudioSource.PlayClipAtPoint(lastClip, soundLocation, lastClipVolume);
        }
    }
}
