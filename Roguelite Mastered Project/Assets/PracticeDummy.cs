using System;
using UnityEngine;
using UnityEngine.UI;

public class PracticeDummy : MonoBehaviour
{
    [SerializeField] private PlayerStats dummyStats;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        dummyStats.SetMaxHealth();
        healthSlider.maxValue = dummyStats.MAXHealth;
    }

    private void Update()
    {
        healthSlider.value = dummyStats.Health;
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (dummyStats.Health > 0)
            {
                dummyStats.TakeDamage(5);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}
