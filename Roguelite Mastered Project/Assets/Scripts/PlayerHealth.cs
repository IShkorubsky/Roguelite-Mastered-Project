using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Player
{
    [SerializeField] private Slider healthBar;
    
    #region Health

    private void OnEnable()
    {
        playerStats.SetMaxHealth();
    }

    private void Update()
    {
        healthBar.value = playerStats.Health * 0.01f;
        //playerStats.HealthRegeneration();

        if (playerStats.Health <= 0)
        {
            //Handle death
            Debug.Log("Player Dead!");
        }
    }

    #endregion
}
