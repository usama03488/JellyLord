using System;
using System.Collections;
using System.Collections.Generic;
using JellyCube;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text levelNumberVictory;
    public TMP_Text levelNumberDefeat;
    public TMP_Text levelNumberPause;
    public TMP_Text timeText;
    
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public GameObject pausePanel;

    public ButtonToggle musicToggle;
    public ButtonToggle sfxToggle;
    
    public LevelClear levelClearManager;
    public SoundManager soundManager;

    public List<GameObject> starsObject;

    
    private void Start()
    {
        levelNumberVictory.text = "Level " + levelClearManager.levelNumber;
        levelNumberDefeat.text = "Level " + levelClearManager.levelNumber;
        levelNumberPause.text = "Level " + levelClearManager.levelNumber;
        soundManager = SoundManager.instance;
        
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
    
    public void NextLevel()
    {
        levelClearManager.NextLevel();
    }

    public void RetryLevel()
    {
        levelClearManager.RetryLevel();
    }

    public void BackToMainMenu()
    {
        levelClearManager.MainMenu();
    }

    public void ActivateStars(int stars)
    {
        for (int i = 0; i < stars; i++)
        {
            starsObject[i].SetActive(true);
        }
    }

    public void TogglePauseOff()
    {
        levelClearManager.TogglePause(false);
    }
    
    public void TogglePauseOn()
    {
        levelClearManager.TogglePause(true);
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
