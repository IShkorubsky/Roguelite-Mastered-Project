using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : PlayerAnimator
    {
        [SerializeField] private Slider healthBar;
        private float _playerHealth;
    
        #region Health

        private void Start()
        {
            GameManager.Instance.ChosenClass.SetMaxHealth();
            healthBar.maxValue = GameManager.Instance.ChosenClass.MAXHealth;
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
        
            healthBar.value = GameManager.Instance.ChosenClass.Health;
        }

        public static void GetDamaged(float damageAmount)
        {
            GameManager.Instance.ChosenClass.GetDamage(damageAmount);
        }

        #endregion
    }
}
