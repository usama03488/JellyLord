using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QualitySetter : MonoBehaviour
{
    public Toggle qualityToggle;
    public string[] names;
    
    private void Start()
    {
        names = QualitySettings.names;
        
        QualityLevel();
    }

    public void ToggleQuality(bool toggle)
    {
        if (toggle)
        {
            QualitySettings.SetQualityLevel(name.Length - 2);
        }
        else
        {
            QualitySettings.SetQualityLevel(name.Length - 5);
        }
    }

    public void QualityLevel()
    {
        var ql = QualitySettings.GetQualityLevel();

        qualityToggle.isOn = ql == name.Length - 2;
        
        Debug.Log(ql);
        Debug.Log(name.Length);
        Debug.Log(name.Length - 2);
    }
}
