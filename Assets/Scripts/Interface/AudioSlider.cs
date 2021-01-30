using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
   public enum Volumes
    {
        MasterVolume,
        EffectVolume,
        MusicVolume,
        VoiceVolume,
    }

   public  Volumes desiredVolume;
   public Slider slider;
    public AudioMixer mixer;

   public float soundValue = 0f;
    private void OnEnable()
    {
        mixer.GetFloat(desiredVolume.ToString(), out soundValue);
        slider.value = soundValue;
    }

    private void Awake()
    {
        slider = this.gameObject.GetComponent<Slider>();
        mixer.GetFloat(desiredVolume.ToString(), out soundValue);
        slider.maxValue = 0f;
        slider.minValue = -70f;
        Debug.Log(soundValue);
    }
    void Start()
    {     
      
    }


   public void ChangeSliderValue()
    {
        mixer.SetFloat(desiredVolume.ToString(), slider.value);
    }
}
