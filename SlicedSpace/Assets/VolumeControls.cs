using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControls : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetSoundsVolume(float sliderValue) {
        mixer.SetFloat("SoundsVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume(float sliderValue) {
        mixer.SetFloat("MusicVolume", (Mathf.Log10(sliderValue) * 20) - 2);
    }
}
