using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Slider dodgeCooldownSlider;
    [SerializeField] private Text enemiesSpawned;
    [SerializeField] private Text currentRound;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject gameOverPanel;
    
    private bool _isGamePaused;
    
    public Slider HealthBarSlider => healthBarSlider;

    public Slider DodgeCooldownSlider => dodgeCooldownSlider;
    
    public bool IsGamePaused => _isGamePaused;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }


    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        currentRound.text = $"Round:{GameManager.Instance.CurrentLevel.ToString()}/5";
        enemiesSpawned.text = $"Enemies left:{GameManager.Instance._enemiesSpawned.ToString()}";
    }

    public void PauseGame()
    {
        _isGamePaused = true;
        inGamePanel.SetActive(false);
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    
    public void UnpauseGame()
    {
        _isGamePaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        inGamePanel.SetActive(true);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        inGamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void Victory()
    {
        Time.timeScale = 0;
        inGamePanel.SetActive(false);
        victoryPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    
    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
