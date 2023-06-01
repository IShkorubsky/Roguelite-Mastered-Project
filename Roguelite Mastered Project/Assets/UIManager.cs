using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Slider dodgeCooldownSlider;
    [SerializeField] private Text enemiesSpawned;
    [SerializeField] private Text currentRound;

    public Slider HealthBarSlider => healthBarSlider;

    public Slider DodgeCooldownSlider => dodgeCooldownSlider;
    
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
}
