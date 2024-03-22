using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private string masterVolumeParam;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float multiplier = 30f;

    private void Awake()
    {
        volumeSlider.onValueChanged.AddListener(HandleSliderValueChanged);
    }
    
    /// <summary>
    /// Handles what happens when the slider value is changed
    /// </summary>
    /// <param name="value"></param>
    private void HandleSliderValueChanged(float value)
    {
        audioMixer.SetFloat(masterVolumeParam, Mathf.Log10(value) * multiplier);
    }
}
