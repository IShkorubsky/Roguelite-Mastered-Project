using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]private GameObject mainMenuPanel;
    [SerializeField]private GameObject settingsMenuPanel;
    [SerializeField]private SceneFading sceneFading;
    [SerializeField]private AudioSource uiClickSFX;
    
    private void Start()
    {
        if (!Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
    }
    
    /// <summary>
    /// Used to show main menu screen
    /// </summary>
    public void ShowMainMenu()
    {
        uiClickSFX.Play();
        settingsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    
    /// <summary>
    /// Used to show the settings menu
    /// </summary>
    public void EnableSettingsMenu()
    {
        uiClickSFX.Play();
        mainMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }
}