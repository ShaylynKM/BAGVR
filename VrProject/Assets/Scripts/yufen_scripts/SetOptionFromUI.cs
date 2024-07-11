using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class SetOptionFromUI : MonoBehaviour
{
    public Scrollbar globalVolumeSlider;
    public Scrollbar musicVolumeSlider;
    public Scrollbar sfxVolumeSlider;
    public TMP_Dropdown turnDropdown;
    public SetTurnTypeFromPlayerPref turnTypeFromPlayerPref;

    private void Start()
    {
        globalVolumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        turnDropdown.onValueChanged.AddListener(SetTurnPlayerPref);

        // Initialize sliders with stored values if they exist
        if (PlayerPrefs.HasKey("globalVolume"))
            globalVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("globalVolume"));

        if (PlayerPrefs.HasKey("musicVolume"))
            musicVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("musicVolume"));

        if (PlayerPrefs.HasKey("sfxVolume"))
            sfxVolumeSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("sfxVolume"));

        if (PlayerPrefs.HasKey("turn"))
            turnDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("turn"));
    }

    public void SetGlobalVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("globalVolume", value); // Save volume setting
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetMusicVolume(value);
        PlayerPrefs.SetFloat("musicVolume", value); // Save music volume setting
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("sfxVolume", value); // Save sfx volume setting
    }

    public void SetTurnPlayerPref(int value)
    {
        PlayerPrefs.SetInt("turn", value);
        turnTypeFromPlayerPref.ApplyPlayerPref();
    }
}
