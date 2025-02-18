using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggle : MonoBehaviour
{
    public GameObject statusBackground;
    public GameObject onObject;
    public GameObject offObject;
    
    public void ToggleButton(bool toggle)
    {
        statusBackground.SetActive(toggle);
        onObject.SetActive(toggle);
        offObject.SetActive(!toggle);
    }
}
