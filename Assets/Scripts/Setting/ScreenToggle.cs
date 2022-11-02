using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenToggle : MonoBehaviour
{
    public Sprite buttonOn;
    public Sprite buttonOff;

    [Tooltip("The PlayerPrefs name of the sound")]
    public string playerPref = "Screen";

    // The index of the current value of the sound
    internal int currentState = 0;
    Image buttonImage;

    void Awake()
    {
        buttonImage = GetComponent<Image>();

        // Get the current state of the screen from PlayerPrefs
        currentState = PlayerPrefs.GetInt(playerPref, 0);

        // Set the screen
        SetScreen();
    }
    void SetScreen()
    {
        // Set the screen in the PlayerPrefs
        PlayerPrefs.SetInt(playerPref, currentState);

        // Update the graphics of the button image to fit the screen state
        if (currentState == 0)
        {
            buttonImage.sprite = buttonOn;
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            buttonImage.sprite = buttonOff;
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    /// <summary>
    /// Toggle the screen. Cycle through all screen modes and set the volume and icon accordingly
    /// </summary>
    public void ToggleScreen()
    {
        currentState = 1 - currentState;

        SetScreen();
    }
}
