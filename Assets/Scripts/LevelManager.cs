using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject LevelsPanel;
    public GameObject SettingsPanel;
    public GameObject MissionsPanel;
    
    
    public RectTransform LevelsParent;
    public GameObject LevelPrefab;

    
    public int totalChapterOneScenes;
    public List<SceneField> chapterOneScenes;
    public List<LevelSelection> chapterOneLevels;

   

    private void Start()
    {
        totalChapterOneScenes = chapterOneScenes.Count;
        SaveManager.instance.LoadFile();
        
        //Call this function if there is only 1 Chapter, Else call the function when you press the button to go to level selection panel.  
        InstantiateChapterOneScenePrefabs();

    }

    [ContextMenu("SaveData")]
    public void SaveGame()
    {
        SaveManager.instance.SaveFile();
    }

    [ContextMenu("DeleteData")]
    public void DeleteProgress()
    {
        SaveManager.instance.DeleteProgress();
    }
    
    [ContextMenu("ISP")]
    public void InstantiateChapterOneScenePrefabs()
    {

        foreach (var tfm in LevelsParent.GetComponentsInChildren<LevelSelection>())
        {
            Destroy(tfm.gameObject);
        }
        
        chapterOneLevels.Clear();
        
        
        foreach (var sf in chapterOneScenes)
        {
            var level = Instantiate(LevelPrefab, LevelsParent);
            var ls = level.GetComponent<LevelSelection>();
            ls.sceneField = sf;
            ls.unlockedLevelTxt.text = sf.SceneName.Replace("__"," ").Replace("Scenes/", "");
            ls.lockedLevelTxt.text = sf.SceneName.Replace("__"," ").Replace("Scenes/", "");
            ls.levelButton.interactable = false;
            ls.Blocked.SetActive(true);
            chapterOneLevels.Add(ls);
        }

        if (SaveManager.instance.saveData.levels.chapter >= Chapters.Chapter2)
        {
            foreach (var chpOneLevels in chapterOneLevels)
            {
                chpOneLevels.levelButton.interactable = true;
                chpOneLevels.Blocked.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < chapterOneLevels.Count; i++)
            {
                if (i <= SaveManager.instance.saveData.levels.completedLevel)
                {
                    chapterOneLevels[i].levelButton.interactable = true;
                    chapterOneLevels[i].Blocked.SetActive(false);
                }
            }
        }

        for (int i = 0; i < SaveManager.instance.saveData.stars.Count; i++)
        {
            for (int j = 0; j < SaveManager.instance.saveData.stars[i].star; j++)
            {
                chapterOneLevels[i].FullStars[j].SetActive(true);
            }
        }
    }
}
