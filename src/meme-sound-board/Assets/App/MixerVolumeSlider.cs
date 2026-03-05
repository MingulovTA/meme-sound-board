using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(Slider))]
public class MixerVolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string exposedParameter = "Volume";

    private Slider slider;
    
    private void Awake()
    {
        slider = GetComponent<Slider>();

        slider.minValue = 0.0001f;
        slider.maxValue = 1f;

        slider.onValueChanged.AddListener(SetVolume);
    }

    private void Start()
    {
        SetVolume(slider.value);
    }

    private void SetVolume(float value)
    {
        //mixer.SetFloat(exposedParameter, Mathf.Lerp(-80f, 0f, value));
        float dB = Mathf.Log10(value) * 20f;
        mixer.SetFloat(exposedParameter, dB);
    }
}