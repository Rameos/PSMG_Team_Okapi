using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    private bool showGUI = false;

    private int selectedIndex = 0;

    private GUIText[] guiTexts;
    	
	void Start () {
        InitRefs();
        PositionGUITexts();

        GlobalEvents.OnPause += EnableMenu;
        GlobalEvents.OnUnPause += DisableMenu;
	}

    private void InitRefs()
    {
        guiTexts = gameObject.GetComponentsInChildren<GUIText>();     
    }

    private void PositionGUITexts()
    {
        int[] yOffsets = { 40, 0, -40 };

        for (int i = 0; i < guiTexts.Length; i++)
        {
            guiTexts[i].pixelOffset = new Vector2(Screen.width / 2, Screen.height / 2 + yOffsets[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (showGUI)
        {
            HighlightGUIText();
            ProcessInputs();
        }
	}

    private void HighlightGUIText()
    {
        for (int i = 0; i < guiTexts.Length; i++)
        {
            if (i == selectedIndex)
            {
                guiTexts[i].material.color = Color.red;
            }
            else
            {
                guiTexts[i].material.color = Color.white;
            }
        }
    }

    private void ProcessInputs()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex = (selectedIndex + 1) % guiTexts.Length;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex--;
            if (selectedIndex < 0)
            {
                selectedIndex = guiTexts.Length - 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))
        {
            switch (selectedIndex)
            {
                case 0:
                    PauseController.Instance.UnPause();
                    break;
                case 1:
                    GlobalEvents.TriggerOnPlayerDeath();
                    PauseController.Instance.UnPause();
                    break;
                case 2:
                    Application.Quit();
                    break;
            }
        }
    }

    private void SetMenuEnabled(bool isEnabled)
    {
        showGUI = isEnabled;
        foreach (GUIText text in transform.GetComponentsInChildren<GUIText>())
        {
            text.enabled = isEnabled;
        }
    }

    public void EnableMenu()
    {
        SetMenuEnabled(true);
    }

    public void DisableMenu()
    {
        SetMenuEnabled(false);
    }

    void OnDestroy()
    {
        // Remove delegates from previous world instance
        GlobalEvents.OnPause -= EnableMenu;
        GlobalEvents.OnUnPause -= DisableMenu;
    }
}
