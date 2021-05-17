using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Serializable]
    public class Sound
    {
        public string name;
        public AudioSource source;
    }

    [Header("Audio source lists")]
    public Sound[] MusicList;
    [Space]
    public Sound[] SoundList;

    [Header("Managers")]
    public SettingSaving SettingsManager;


    public static AudioManager instance;

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        InstantiateAudioVolume();
        PlayMenuMusic();
    }

    public void InstantiateAudioVolume()
    {
        ChangeMusicVolume(SettingsManager.SettingsObject.MusicVolume);
        //ChangeSoundVolume(SettingsManager.SettingsObject.SoundVolume);
    }

    //---------------------------------------------

    public void PlayMenuMusic()
    {
        PlayMusic("Menu");
    }

    public void ChangeMusic(string name)
    {
        StopMusic();
        PlayMusic(name);
    }

    private void PlayMusic(string name)
    {
        foreach (var track in MusicList)
        {
            if (track.name == name)
            {
                track.source.loop = true;
                track.source.Play();
                return;
            }
        }
    }
    private void StopMusic()
    {
        foreach (var track in MusicList)
        {
            if (track.source.isPlaying)
            {
                track.source.Stop();
                return;
            }
        }
    }

    //----------------------------------------------

    public void ChangeMusicVolume(float value)
    {
        foreach (var track in MusicList)
        {
            track.source.volume = value;
        }
    }
    public void ChangeSoundVolume(float value)
    {
        foreach (var track in SoundList)
        {
            track.source.volume = value;
        }
    }
}
