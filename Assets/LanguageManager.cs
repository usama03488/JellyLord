using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public int languageIndex;
    public TMP_Text languageText;

    public List<string> languages;

    private void Start()
    {
        languageText.text = languages[languageIndex];
    }

    public void CycleLanguages(bool next)
    {
        if (next)
        {
            languageIndex++;
            if (languageIndex >= languages.Count)
            {
                languageIndex = 0;
            }
        }
        else
        {
            languageIndex--;
            if (languageIndex < 0)
            {
                languageIndex = languages.Count - 1;
            }
        }
        
        languageText.text = languages[languageIndex];
    }
}
