using UnityEngine;
using System.Collections;

public class StepsController : MonoBehaviour {
    public string actualState;

    private PlayerStatus status;

	// Use this for initialization
	void Start () {
        status = GetComponent<PlayerStatus>();
        actualState = status.getState();
	}
	
	// Update is called once per frame
	void Update () {
        actualState = status.getState();
        switch(actualState)
        {
            case "none":
                {
                    audio.Stop();
                    break;
                }
            case "walk":
                {
                    if(!audio.isPlaying)
                    {
                        audio.Play();
                    }
                    break;
                }
            case "traverse":
                {
                    if (!audio.isPlaying)
                    {
                        audio.Play();
                    }
                    break;
                }
        }
	
	}
}
