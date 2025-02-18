using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialsChangeManager : MonoBehaviour
{
    public List<GameObject> tutorials;
    int pointer = 0;

    public GameObject next;
    public GameObject prev;

    // Update is called once per frame
    void Update()
    {
        if (pointer == 0)
            prev.SetActive(false);

        else if (pointer == tutorials.Count - 1)
            next.SetActive(false);
        
        else {
            next.SetActive(true);
            prev.SetActive(true);
        }
            
        
    }

    public void nextTutorial()
    {
        tutorials[pointer].SetActive(false);

        pointer++;
        
        tutorials[pointer].SetActive(true);
           
    }

    public void prevTutorial()
    {
        tutorials[pointer].SetActive(false);

        pointer--;

        tutorials[pointer].SetActive(true);
    }
}
