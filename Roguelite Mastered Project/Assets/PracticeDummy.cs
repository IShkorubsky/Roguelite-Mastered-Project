using UnityEngine;
using UnityEngine.UI;

public class PracticeDummy : MonoBehaviour
{
    [SerializeField] private PlayerStats dummyStats;
    [SerializeField] private Slider healthSlider;

    public PlayerStats DummyStats => dummyStats;

    private void Start()
    {
        DummyStats.SetMaxHealth();
        healthSlider.maxValue = DummyStats.MAXHealth;
    }

    private void Update()
    {
        healthSlider.value = DummyStats.Health;

        if (DummyStats.Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
