using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button levelButton;
    public GameObject[] FullStars;
    public GameObject[] EmptyStars;
    public GameObject Blocked;
    public GameObject NewLevel;
    public TMP_Text unlockedLevelTxt;
    public TMP_Text lockedLevelTxt;
    public SceneField sceneField;
    public SceneField tutorialScene;
    

    

    public void OpenScene()
    {
         SceneManager.LoadSceneAsync(SaveManager.instance.saveData.tutorialDone ? sceneField : tutorialScene);
       // SceneManager.LoadSceneAsync("Level__Tut");
    }
}
