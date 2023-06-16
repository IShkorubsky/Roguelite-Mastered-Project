using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : PlayerAnimator
{
    [SerializeField] private Slider healthBar;
    private float _playerHealth;
    
    #region Health

    private void Awake()
    {
        healthBar = UIManager.Instance.HealthBarSlider;
        
        if (GameManager.Instance.ChosenClass != null)
        {
            GameManager.Instance.ChosenClass.SetMaxHealth();
        }
    }

    private void Update()
    {
        //playerStats.HealthRegeneration();

        if (GameManager.Instance.ChosenClass.Health <= 0)
        {
            //Handle death
            Debug.Log("Player Dead!");
        }
        
        if (!healthBar)
        {
            return;
        }
        
        healthBar.value = GameManager.Instance.ChosenClass.Health * 0.01f;
    }

    public void GetDamaged()
    {
        
    }

    #endregion
}
