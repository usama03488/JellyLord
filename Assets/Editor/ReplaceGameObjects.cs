using UnityEngine;
using UnityEditor;
using System.Collections;
// CopyComponents - by Michael L. Croswell for Colorado Game Coders, LLC
// March 2010
 
public class ReplaceGameObjects : ScriptableWizard
{
    public bool copyValues = true;
    public GameObject NewType;
    public GameObject[] OldObjects;
 
    [MenuItem("Custom/Replace GameObjects %#&R")]
 
 
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard("Replace GameObjects", typeof(ReplaceGameObjects), "Replace");
    }
 
    void OnWizardCreate()
    {
        //Transform[] Replaces;
        //Replaces = Replace.GetComponentsInChildren<Transform>();
 
        foreach (GameObject go in OldObjects)
        {
            GameObject newObject;
            newObject = (GameObject)EditorUtility.InstantiatePrefab(NewType);
            newObject.transform.position = go.transform.position;
            newObject.transform.rotation = go.transform.rotation;
            newObject.transform.parent = go.transform.parent;
 
            DestroyImmediate(go);
 
        }
 
    }
}
