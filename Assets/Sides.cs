using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sides : MonoBehaviour
{
    public AudioSource audioSource;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tiles"))
        {
            audioSource.Play();
        }
    }
}
