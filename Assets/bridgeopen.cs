using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeopen : MonoBehaviour
{
    public Animator bridge;

    public GameObject disabler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            bridge.SetBool("open",true);
            disabler.SetActive(false);
        }
        
    }
}
