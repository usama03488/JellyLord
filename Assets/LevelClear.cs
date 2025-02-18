using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JellyCube;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class LevelClear : MonoBehaviour
{
    public Chapters chapter;
    public int levelNumber;

    public UIManager uiManager;
    public Transform cube;    
    public Vector3 adjustment;

    public int starAmount; 
    public float time;
    private bool _calcTime;

    public StarTimes starTimes = new StarTimes();

    private TMP_Text _timeText;
    
    private void Awake()
    {
        foreach (var ffp in cube.GetComponentsInChildren<FreeFallPlayer>())
        {
            ffp.gameOver = uiManager.defeatPanel;
        }
        
        uiManager.levelClearManager = this;
        _timeText = uiManager.timeText;
        _calcTime = true;
    }

    private void Start()
    {
      //  FindObjectOfType<GoogleAdMobController>().RequestAndLoadInterstitialAd();
    }

    private void Update()
    {
        if (_calcTime)
        {
            time += Time.deltaTime;
            // _timeText.text = time.ToString();
        }
    }

    IEnumerator WaitAndGo()
    {
        _calcTime = false;

        if (chapter == Chapters.Chapter1 && levelNumber == 0)
        {
            yield return new WaitForSeconds(1.5f);
            
            starAmount = 3;
            
            uiManager.ActivateStars(starAmount);

            uiManager.victoryPanel.SetActive(true);
            gameObject.SetActive(false);
            
            SaveManager.instance.SaveFile();
        }
        else
        {
            if (time <= starTimes.threeStar)
            {
                starAmount = 3;
            }
            else if (time > starTimes.threeStar && time <= starTimes.twoStar)
            {
                starAmount = 2;
            }
            else
            {
                starAmount = 1;
            }

            bool foundIt = false;

            foreach (var ss in SaveManager.instance.saveData.stars)
            {
                if (ss.chapter == chapter && ss.level == levelNumber)
                {
                    if (ss.star < starAmount)
                        ss.star = starAmount;
                    foundIt = true;
                }
            }

            if (!foundIt)
            {
                SaveManager.instance.saveData.stars.Add(new Stars(chapter, levelNumber, starAmount));
            }

            yield return new WaitForSeconds(1.5f);

            uiManager.ActivateStars(starAmount);

            uiManager.victoryPanel.SetActive(true);
            gameObject.SetActive(false);

            if (SaveManager.instance.saveData.levels.chapter <= chapter)
            {
                if (SaveManager.instance.saveData.levels.completedLevel <= levelNumber)
                {
                    if (levelNumber == 10)
                    {
                        SaveManager.instance.saveData.levels.chapter = chapter + 1;
                        SaveManager.instance.saveData.levels.completedLevel = 0;
                    }
                    else
                    {
                        SaveManager.instance.saveData.levels.chapter = chapter;
                        SaveManager.instance.saveData.levels.completedLevel = levelNumber;
                    }
                }
            }
            Adsinterstitial.instance.ShowAd();
            SaveManager.instance.SaveFile();
            //FindObjectOfType<GoogleAdMobController>().ShowInterstitialAd();
           // UnityAds.Instance.OnInterstitialLoaded();


        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "VerticalEnd")
        {
            PlayerPrefs.SetInt("Level" + SceneManager.GetActiveScene().buildIndex, 1);
            Destroy(other.transform.root.GetComponent<CubeController>());
            cube.localPosition = adjustment;
            gameObject.transform.position = new Vector3(200, 200, 200);
            other.GetComponentInParent<Rigidbody>().isKinematic = false;
            other.GetComponentInParent<Rigidbody>().useGravity = true;
            StartCoroutine(WaitAndGo());

        }
    }

    public void NextLevel()
    {
        int scene_number = SceneManager.GetActiveScene().buildIndex;
       // Debug.Log(SceneManager.sceneCountInBuildSettings);
        if(scene_number+1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(scene_number + 1);
    }


    public void RetryLevel()
    {
        int scene_number = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene_number);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void TogglePause(bool toggle)
    {
        if (toggle)
        {
            _calcTime = false;
            uiManager.pausePanel.SetActive(true);
            cube.GetComponentInParent<CubeController>().m_CanControl = false;
        }
        else
        {
            _calcTime = true;
            uiManager.pausePanel.SetActive(false);
            cube.GetComponentInParent<CubeController>().m_CanControl = true;
        }
    }
}

[Serializable]
public class StarTimes
{
    public float threeStar;
    public float twoStar;

    public StarTimes()
    {
        threeStar = 0f;
        twoStar = 0f;
    }

    public StarTimes(float s3, float s2)
    {
        threeStar = s3;
        twoStar = s2;
    }

    //IEnumerator LoadAndShowInterstitial()
    //{
    //    UnityAds.Instance.LoadInerstitialAd();
    //    yield return new WaitForSeconds(3f);
    //    UnityAds.Instance.OnInterstitialLoaded();
    //}
}
