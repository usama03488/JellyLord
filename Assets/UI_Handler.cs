using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Handler : MonoBehaviour
{


    public GameObject mainmenu_bg;
    public GameObject selectLevels_bg;
    


    public void select_Levels()
    {
        mainmenu_bg.SetActive(false);
        selectLevels_bg.SetActive(true);
    }

    public void quitgame()
    {
        Application.Quit();
    }

    public void start_game(int scene_num)
    {
        string pref_check = "Level" + scene_num;
        if (PlayerPrefs.GetInt(pref_check) == 1 || scene_num == 1)
            SceneManager.LoadScene(scene_num);
    }
    
    
}
