using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class Skin_adjustment : MonoBehaviour
{

    
    public GameObject cube;

    public GameObject ReferenceCube;



    //public Material[] CubeColors;

    public Image[] Button;
    public Color[] Colors;



    void Start()
    {
        
        SaveManager.instance.LoadFile();
        cube.GetComponentInChildren<MeshRenderer>().sharedMaterial.color = SaveManager.instance.saveData.blockColor;
        ReferenceCube.GetComponentInChildren<MeshRenderer>().sharedMaterial.color = SaveManager.instance.saveData.blockColor;

        for (int i = 0; i < Colors.Length; i++)
        {
            Button[i].color = Colors[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator timedelay()
    {
        
        yield return new WaitForSeconds(0.1f);
        ReferenceCube.transform.position = new Vector3(-6.68f, 0f, 3.78f);
    }
   
    public void SetColor(int x)
    {

        cube.GetComponentInChildren<MeshRenderer>().sharedMaterial.color = Colors[x];
        ReferenceCube.GetComponentInChildren<MeshRenderer>().sharedMaterial.color = Colors[x];

        SaveManager.instance.saveData.blockColor = Colors[x];

        SaveManager.instance.SaveFile();

        //cube.sharedMaterial = CubeColors[x];
        ReferenceCube.transform.position = new Vector3(-6.68f, 0.1f, 3.78f);

        StartCoroutine( timedelay() );

        
    }



}
