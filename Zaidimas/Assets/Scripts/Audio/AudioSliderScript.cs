using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSliderScript : MonoBehaviour
{
    public SliderOption sliderOption;
    public enum SliderOption
    {
        MusicSlider,
        SoundSlider
    }

    public void Start()
    {
        AudioManager audioManager = GameObject.Find("AUDIO OBJECT").GetComponent<AudioManager>();
        //MenuUIManager menuUIManager = GameObject.Find("Menu UI Manager").GetComponent<MenuUIManager>();
        Slider slider = this.gameObject.GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate 
        {
            Debug.Log("Change");
            if (sliderOption == SliderOption.MusicSlider)
                audioManager.ChangeMusicVolume(slider.value);
            else
                audioManager.ChangeSoundVolume(slider.value);
        });

        //slider.onValueChanged.AddListener(delegate { menuUIManager.playClick = true; });

        //if (sliderOption == SliderOption.MusicSlider)
        //{
        //    audioManager.MusicSlider = slider;
        //    slider.value = audioManager.musicSliderValue;
        //}
        //else if (sliderOption == SliderOption.SoundSlider)
        //{
        //    audioManager.SoundSlider = slider;
        //    slider.value = audioManager.soundSliderValue;
        //}
    }
}
