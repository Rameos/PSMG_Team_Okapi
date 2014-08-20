using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {
        
    public bool debug = false;

    private bool isPaused = false;
    private float previousTimeScale = 1;

    private GameObject player;
    private GameObject mainCamera;

    private static PauseController instance;

    public static PauseController Instance
    {
        get
        {
            if (!instance)
            {
                instance = (PauseController)FindObjectOfType(typeof(PauseController));
                {
                    if (!instance)
                    {
                        Debug.LogError("Couldn't find ScreenFader");
                    }
                }
            }

            return instance;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");        
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        if(Input.GetKeyDown("p")) {
            if(!isPaused) {
                Pause();
            } else {
                UnPause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0;
        Debug.Log(player.GetComponent("MouseLook"));
        player.GetComponent<MouseLook>().enabled = false;
        Debug.Log(mainCamera.GetComponent<MouseLook>());
        mainCamera.GetComponent<MouseLook>().enabled = false;
    }

    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = previousTimeScale;
        player.GetComponent<MouseLook>().enabled = true;
        mainCamera.GetComponent<MouseLook>().enabled = true;
    }
}
