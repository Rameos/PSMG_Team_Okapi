using UnityEngine;
using System.Collections;

public class PauseController : MonoBehaviour {
        
    public bool debug = false;

    private bool isPaused = false;
    private float previousTimeScale = 1;

    private MouseLook playerMouseLook;
    private MouseLook mainCameraMouseLook;    

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
                        Debug.LogError("Couldn't find PuaseController");
                    }
                }
            }

            return instance;
        }
    }

    void Start()
    {
        playerMouseLook = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();        
        mainCameraMouseLook = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseLook>();        
    }

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
                
        guiTexture.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
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
        playerMouseLook.enabled = false;        
        mainCameraMouseLook.enabled = false;

        GlobalEvents.TriggerOnPause();

        guiTexture.enabled = true;
    }

    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = previousTimeScale;
        playerMouseLook.enabled = true;
        mainCameraMouseLook.enabled = true;

        GlobalEvents.TriggerOnUnPause();

        guiTexture.enabled = false;
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
