using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceHolder : MonoBehaviour
{
    public FreeFallPlayer freeFallPlayer;
    public SideManager sideManager;

    public void Start()
    {
        freeFallPlayer = GetComponentInChildren<FreeFallPlayer>();
    }
    
    
}
