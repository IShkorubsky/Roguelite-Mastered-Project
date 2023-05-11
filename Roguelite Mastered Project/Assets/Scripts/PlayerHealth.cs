using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : PlayerAnimator
{
    [SerializeField] private Slider healthBar;
    
    #region Health

    private void OnEnable()
    {
        if (!GameManager.Instance)
        {
            GameManager.Instance.ChosenClass.SetMaxHealth();
        }
    }

    private void Awake()
    {
        healthBar = UIManager.Instance.HealthBarSlider;
    }

    private void Update()
    {

        healthBar.value = GameManager.Instance.ChosenClass.Health * 0.01f;
        //playerStats.HealthRegeneration();

        if (GameManager.Instance.ChosenClass.Health <= 0)
        {
            //Handle death
            Debug.Log("Player Dead!");
        }
    }

    #endregion
}
