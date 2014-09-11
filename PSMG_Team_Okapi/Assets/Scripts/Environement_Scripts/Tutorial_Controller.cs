using UnityEngine;
using System.Collections;

public class Tutorial_Controller : MonoBehaviour {

    public enum State { preStart, started, calibrationStart, calibrationEnd, finished, postFinished };
    private State currentState;

    private CharacterMotor playerMotor;
    private Player_Status playerStatus;

    private InkubatorEnemy_TransparencyController[] inkubatorEnemyControllers;

    public delegate void PowerOffEventHandler();
    public event PowerOffEventHandler OnPowerFalloff;

    public bool debug = false;

	// Use this for initialization
	void Start () {
        currentState = State.preStart;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerMotor = player.GetComponent<CharacterMotor>();
        playerStatus = player.GetComponent<Player_Status>();

        inkubatorEnemyControllers = GameObject.FindObjectsOfType<InkubatorEnemy_TransparencyController>();
	}

    public void ChangeState(State newState)
    {
        if (newState == currentState)
        {
            return;
        }

        if (debug)
        {
            Debug.Log("newState: " + newState);
        }

        currentState = newState;

        switch (newState)
        {
            case State.started:
                SetPlayerMovementEnabled(false);
                StartCoroutine(PlayTTS(0, State.calibrationStart));
                break;
            case State.calibrationStart:
                StartCoroutine(Calibration());
                break;
            case State.calibrationEnd:
                RegisterInkubatorCallbacks();
                break;
            case State.finished:                
                SetPlayerMovementEnabled(true);
                StartCoroutine(PlayTTS(1, State.postFinished));
                break;
            case State.postFinished:
                Debug.Log("postFinished");
                OnPowerFalloff();
                break;
        }        
    }

    private void SetPlayerMovementEnabled(bool enabled)
    {
        playerMotor.inputEnabled = enabled;
        playerStatus.inputEnabled = enabled;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (currentState == State.preStart)
        {
            ChangeState(State.started);
        }
    }

    private void OnInkubatorEnemyVisible()
    {
        if (currentState == State.calibrationEnd)
        {            
            ChangeState(State.finished);
            UnregisterInkubatorCallbacks();
        }
    }

    private void RegisterInkubatorCallbacks()
    {
        foreach (InkubatorEnemy_TransparencyController controller in inkubatorEnemyControllers)
        {
            controller.OnInkubatorEnemyVisible += OnInkubatorEnemyVisible;
        }
    }

    private void UnregisterInkubatorCallbacks()
    {
        foreach (InkubatorEnemy_TransparencyController controller in inkubatorEnemyControllers)
        {
            controller.OnInkubatorEnemyVisible -= OnInkubatorEnemyVisible;
        }
    }

    private IEnumerator Calibration()
    {
        // start eye tracker calibration

        yield return new WaitForSeconds(3);

        ChangeState(State.calibrationEnd);
    }

    private IEnumerator PlayTTS(int index, State nextState)
    {
        AudioSource[] sources = gameObject.GetComponents<AudioSource>();

        sources[index].Play();

        yield return new WaitForSeconds(sources[index].clip.length);

        ChangeState(nextState);
    }
}
