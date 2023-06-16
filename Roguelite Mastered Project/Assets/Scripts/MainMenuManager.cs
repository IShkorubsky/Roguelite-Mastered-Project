using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject settingsMenuPanel;
    [SerializeField] private SceneFading sceneFading;
    //[SerializeField] private AudioSource uiClickSFX;

    private void Start()
    {
        Time.timeScale = 1;
        
        if (!Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Used to show main menu screen
    /// </summary>
    public void ShowMainMenu()
    {
        //uiClickSFX.Play();
        settingsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }
    
    /// <summary>
    /// Used to show the settings menu
    /// </summary>
    public void EnableSettingsMenu()
    {
        //uiClickSFX.Play();
        mainMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }
    
    /// <summary>
    /// Deletes Player Prefs
    /// </summary>
    public void DeleteAllPlayerPrefs()
    {
        //uiClickSFX.Play();
        PlayerPrefs.DeleteAll();
    }
    
        
    /// <summary>
    /// Used to quit the game
    /// </summary>
    public void Quit()
    {
        //uiClickSFX.Play();
        Application.Quit();
    }
}
