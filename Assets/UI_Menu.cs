using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Menu : MonoBehaviour
{
    public GameObject canvas_bg;
    public GameObject canvas_text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "VerticalEnd" || other.tag == "Player")
        {
            canvas_bg.SetActive(true);
            canvas_text.SetActive(true);
        }
    }
}
