using UnityEngine;
using System.Collections;
using iViewX;

public class GeneratorGazeFocus : MonoBehaviourWithGazeComponent {

    public float fadeSpeed = 0.3f;
    public bool debug = false;
    public float waitTime;

    private bool endingSequenceFinished = false;
    private bool fadeOutStarted = false;

	void Start () {
        Generator_Switch_Controller switchController = GameObject.FindObjectOfType<Generator_Switch_Controller>();
        if (switchController != null)
        {
            switchController.OnActivateGeneratorSwitch += OnGeneratorActivated;
        }
	}

    public void OnGeneratorActivated(GameObject generator)
    {
        StartCoroutine(EndingSequence());
    }

    public override void OnGazeEnter(RaycastHit hit)
    {
        if (debug)
        {
            Debug.Log("Enter");
        }
    }

    public override void OnGazeStay(RaycastHit hit)
    {
        if (endingSequenceFinished && !fadeOutStarted)
        {
            fadeOutStarted = true;
          

            GlobalEvents.OnScreenFadeOutComplete += delegate
            {
                if (debug)
                {
                    Debug.Log("Fade to white complete!");
                }

                EnvironmentVars.restartFromEnding = true;
                Application.LoadLevel("main_menu");                
            };

            ScreenFader.Instance.fadeSpeed = fadeSpeed;
            ScreenFader.Instance.FadeToWhite();
        }
    }

    public override void OnGazeExit()
    {
        if (debug)
        {
            Debug.Log("Exit");
        }
    }

    private IEnumerator EndingSequence()
    {
        Debug.Log("Starting Ending Sequence!");        

        yield return new WaitForSeconds(waitTime);

        endingSequenceFinished = true;
    }
}
