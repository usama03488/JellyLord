using System;
using System.Collections;
using System.Collections.Generic;
using JellyCube;
using UnityEngine;

public class DyingTiles : MonoBehaviour
{

    public GameObject pref;
    public GameObject cube;
    public Vector3 position;
    
    
    // Start is called before the first frame update


    private void Start()
    {
        position = gameObject.transform.position;
        position.y = 1.01f;
        cube = CubeController.instance.m_Cube.gameObject; //GameObject.Find("Cube");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VerticalEnd"))
        {
            other.transform.root.GetComponent<ReferenceHolder>().freeFallPlayer.RestartLevel();
            cube.transform.position = position;
            
            other.GetComponentInParent<Rigidbody>().isKinematic = false;
            other.GetComponentInParent<Rigidbody>().useGravity = true;
            pref.gameObject.SetActive(false);
            Destroy(other.transform.root.GetComponent<CubeController>());
        }
    }
}
