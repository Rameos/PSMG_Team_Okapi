using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public Color notSelectedColor = Color.white;
    public Color selectedColor1 = Color.white;
    public Color selectedColor2 = Color.red;

    private int selectedIndex = 0;
    private float pulseCounter = 0;
    public float pulseRate = 5.0f;

    private bool menuActive = true;

    private GUIText[] guiTexts;

    void Start()
    {
        InitRefs();
        PositionGUITexts();
    }

    private void InitRefs()
    {
        guiTexts = gameObject.GetComponentsInChildren<GUIText>();
    }

    private void PositionGUITexts()
    {
        int[] yOffsets = { 40, -40 };

        for (int i = 0; i < guiTexts.Length; i++)
        {
            guiTexts[i].pixelOffset = new Vector2(Screen.width / 8, Screen.height / 1.5f + yOffsets[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HighlightGUIText();
        ProcessInputs();
    }

    private void HighlightGUIText()
    {
        pulseCounter += (pulseRate / 100);

        for (int i = 0; i < guiTexts.Length; i++)
        {
            if (i == selectedIndex)
            {
                guiTexts[i].material.color = Color.Lerp(selectedColor1, selectedColor2, Mathf.Sin(pulseCounter) * 0.5f + 0.5f);
            }
            else
            {
                guiTexts[i].material.color = notSelectedColor;
            }
        }
    }

    private void ProcessInputs()
    {
        if (!menuActive)
        {
            return;
        }

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
            menuActive = false;

            switch (selectedIndex)
            {
                case 0:
                    GlobalEvents.OnScreenFadeOutComplete += delegate
                    {
                        Application.LoadLevel("Level_one");
                    };
                    ScreenFader.Instance.FadeToBlack();
                    AudioFader.Instance.FadeOut();
                    break;
                case 1:
                    Application.Quit();
                    break;
            }
        }
    }
}
