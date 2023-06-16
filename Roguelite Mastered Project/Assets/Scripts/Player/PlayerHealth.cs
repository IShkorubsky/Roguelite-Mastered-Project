using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : PlayerAnimator
{
    [SerializeField] private Slider healthBar;
    private float _playerHealth;
    
    #region Health

    private void Start()
    {
        GameManager.Instance.ChosenClass.SetMaxHealth();
    }

    private void Update()
    {
        //playerStats.HealthRegeneration();

        if (GameManager.Instance.ChosenClass.Health <= 0)
        {
            UIManager.Instance.GameOver();
        }
        
        if (!healthBar)
        {
            return;
        }
        
        healthBar.value = GameManager.Instance.ChosenClass.Health * 0.01f;
    }

    public void GetDamaged(float damageAmount)
    {
        GameManager.Instance.ChosenClass.GetDamage(damageAmount);
    }

    #endregion
}
