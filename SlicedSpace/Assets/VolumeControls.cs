using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControls : MonoBehaviour
{
    [SerializeField]
    public AudioMixer mixer;
    public string outputName;

    void Awake() {
        this.mixer.GetFloat(this.outputName, out float value);
        if(this.outputName == "SoundsVolume") {
            GetComponent<Slider>().value = Mathf.Pow(10, value / 20);
        }else if(this.outputName == "MusicVolume") {
            GetComponent<Slider>().value = Mathf.Pow(10, (value + 2) / 20);
        }
        
    }

    public void SetSoundsVolume(float sliderValue) {
        mixer.SetFloat("SoundsVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume(float sliderValue) {
        mixer.SetFloat("MusicVolume", (Mathf.Log10(sliderValue) * 20) - 2);
    }
}
