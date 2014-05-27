using UnityEngine;
using System.Collections;

public class Player_StepsController : MonoBehaviour {
    public string actualState;

    private Player_Status status;

	// Use this for initialization
	void Start () {
        status = GetComponent<Player_Status>();
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
