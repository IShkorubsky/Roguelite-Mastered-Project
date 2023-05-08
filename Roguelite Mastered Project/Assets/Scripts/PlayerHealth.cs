using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : PlayerAnimator
{
    [SerializeField] private Slider healthBar;
    
    #region Health

    private void OnEnable()
    {
        if (!GameController.Instance)
        {
            GameController.Instance.ChosenClass.SetMaxHealth();
        }
    }

    private void Update()
    {
        healthBar.value = GameController.Instance.ChosenClass.Health * 0.01f;
        //playerStats.HealthRegeneration();

        if (GameController.Instance.ChosenClass.Health <= 0)
        {
            //Handle death
            Debug.Log("Player Dead!");
        }
    }

    #endregion
}
