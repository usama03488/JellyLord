using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockvisibility : MonoBehaviour
{
   public GameObject block;
   public GameObject inviswall;
   
   private void OnTriggerEnter(Collider other)
   {
      if(other.tag== "Player")
         inviswall.gameObject.SetActive(false);
         block.gameObject.SetActive(true);
   }
}
