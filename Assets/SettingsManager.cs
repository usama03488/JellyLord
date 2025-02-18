using System;
using System.Collections;
using System.Collections.Generic;
using JellyCube;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public SoundManager soundManager;
    public ButtonToggle musicToggle;
    public ButtonToggle sfxToggle;

    public void Start()
    {
        CheckMixer();
    }

    public void CheckMixer()
    {
        float vol;
        
        soundManager.audioMixer.GetFloat("Music", out vol);
        musicToggle.ToggleButton(vol > -40);
        
        soundManager.audioMixer.GetFloat("SFX", out vol);
        sfxToggle.ToggleButton(vol > -40);
    }
    
    public void ToggleMusic(bool toggle)
    {
        if (toggle)
        {
            soundManager.audioMixer.SetFloat("Music", 0f);
        }
        else
        {
            soundManager.audioMixer.SetFloat("Music", -80f);
        }
    }

    public void ToggleSoundEffects(bool toggle)
    {
        if (toggle)
        {
            soundManager.audioMixer.SetFloat("SFX", 0f);
        }
        else
        {
            soundManager.audioMixer.SetFloat("SFX", -80f);
        }
    }
}
