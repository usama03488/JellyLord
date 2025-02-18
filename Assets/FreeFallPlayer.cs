using System;
using System.Collections;
using System.Collections.Generic;
using JellyCube;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeFallPlayer : MonoBehaviour
{
    [Header("Center Of Mass Location")] public Vector3 com;

    public GameObject player;
    public GameObject gameOver;
    public TutorialManager tutorialObject;
    int prob;
    private void Start()
    {
        prob=PlayerPrefs.GetInt("Prob");
    }
    public void RestartLevel()
    {
        
        StartCoroutine(RestartWait());
    }

    IEnumerator RestartWait()
    {
        if (tutorialObject != null)
        {
            yield return new WaitForSeconds(2);
            tutorialObject.RestartTutorial();
        }
        else
        {
            yield return new WaitForSeconds(1.5f);
            gameOver.SetActive(true);
            prob += 1;
            Debug.Log(prob);
            if (prob > 2)
            {
                prob = 0;
                
                PlayerPrefs.SetInt("Prob", prob);
                Invoke(nameof(LoadAd), 0.5f);
            }
            else
            {
                PlayerPrefs.SetInt("Prob", prob);
            }
        }
    }
    void LoadAd()
    {

        

        Adsinterstitial.instance.ShowAd();



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroyer"))
        {
            
            Debug.Log(this.name);
            other.GetComponent<BoxCollider>().enabled = false;
            // com = new Vector3(1.24f, 0.16f, 0);
            player.GetComponent<Rigidbody>().centerOfMass = com;
            player.GetComponent<Rigidbody>().isKinematic = false;
            player.GetComponent<Rigidbody>().useGravity = true;
            Destroy(this.transform.root.GetComponent<CubeController>());
            StartCoroutine(RestartWait());
        }
    }


    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawSphere(transform.position + transform.rotation * com, 0.1f);
    // }
}
