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
}
