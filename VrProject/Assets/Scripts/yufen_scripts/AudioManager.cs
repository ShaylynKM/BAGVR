using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0, 1)]
    public float volume = 1;
    [Range(-3, 3)]
    public float pitch = 1;
    public bool loop = false;
    public bool playOnAwake = false;
    [HideInInspector]
    public AudioSource source;

    public Sound()
    {
        volume = 1;
        pitch = 1;
        loop = false;
    }
}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioSource[] soundEffects; // Array for short sound effects

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            if (!s.source)
                s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;

            if (s.playOnAwake)
                s.source.Play();
        }

        LoadVolumeSettings();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Stop();
    }

    public void PlaySFX(int sfxToPlay)
    {
        if (sfxToPlay < 0 || sfxToPlay >= soundEffects.Length)
        {
            Debug.LogWarning("SFX index out of range: " + sfxToPlay);
            return;
        }

        soundEffects[sfxToPlay].Stop();
        soundEffects[sfxToPlay].Play();
    }

    public void PlaySFXPitched(int sfxToPlay)
    {
        if (sfxToPlay < 0 || sfxToPlay >= soundEffects.Length)
        {
            Debug.LogWarning("SFX index out of range: " + sfxToPlay);
            return;
        }

        soundEffects[sfxToPlay].pitch = UnityEngine.Random.Range(.8f, 1.2f);
        PlaySFX(sfxToPlay);
    }

    public void SetMusicVolume(float volume)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = volume;
        }
    }

    public void SetSFXVolume(float volume)
    {
        foreach (AudioSource sfx in soundEffects)
        {
            sfx.volume = volume;
        }

    }

    public void SetMusicPitch(string name, float pitch)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.pitch = pitch;
    }


    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float musicVolume = PlayerPrefs.GetFloat("musicVolume");
            SetMusicVolume(musicVolume);
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            float sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
            SetSFXVolume(sfxVolume);
        }
    }
}
