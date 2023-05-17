using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private string masterVolumeParam = "MasterVolume";
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private float multiplier = 30f;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(masterVolumeParam, volumeSlider.value);
    }
    
    private void Awake()
    {
        volumeSlider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(masterVolumeParam,volumeSlider.value);
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
