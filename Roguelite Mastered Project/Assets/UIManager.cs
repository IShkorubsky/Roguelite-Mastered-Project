using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    [SerializeField] private Slider healthBarSlider;
    [SerializeField] private Slider dodgeCooldownSlider;

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
}
