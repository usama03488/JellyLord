using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideManager : MonoBehaviour
{
    public List<Sides> sides;

    private void Start()
    {
        foreach (var side in sides)
        {
            side.audioSource = GetComponent<AudioSource>();
        }
    }
}
