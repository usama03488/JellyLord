using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JellyCube;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public CubeController cubeController;
    public UIManager uiManager;
    public GameObject tutorialCanvas;
    public GameObject uiCanvas;

    public GameObject firstPanel;

    private void Start()
    {
        if (SaveManager.instance.saveData.tutorialDone) return;
        
        firstPanel.SetActive(true);
        //tutorialCanvas.SetActive(true);
        uiCanvas.SetActive(false);
        cubeController.m_CanControl = false;
    }

    public void EndTutorial()
    {
        tutorialCanvas.SetActive(false);
        uiCanvas.SetActive(true);
        cubeController.m_CanControl = true;
        SaveManager.instance.saveData.tutorialDone = true;
    }

    public void RestartTutorial()
    {
        uiManager.RetryLevel();
    }
}
