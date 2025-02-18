using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    
    public static SaveManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }

    #endregion
    
    public SaveData saveData = new SaveData();
    
    
    [ContextMenu("SaveData")]
    public void SaveFile()
    {
        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(Application.persistentDataPath + "/SaveData.json", json);
    }

    
    [ContextMenu("LoadData")]
    public void LoadFile()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/SaveData.json");
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saveData = new SaveData();
        }
    }
    
    [ContextMenu("Delete Files")]
    public void DeleteProgress()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.json"))
        {
            File.Delete(Application.persistentDataPath + "/SaveData.json");

            saveData = new SaveData();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            saveData = new SaveData();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}
