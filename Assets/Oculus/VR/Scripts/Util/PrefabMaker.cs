using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrefabMaker : MonoBehaviour
{
    // public GameObject Testobjects;
    // Start is called before the first frame update
    public void cretePrefab(GameObject PrefabObjects)
    {        
        string localPath = "Assets/" + PrefabObjects.name + ".prefab";
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        
        bool prefabSucces;
        PrefabUtility.SaveAsPrefabAsset(PrefabObjects, localPath, out prefabSucces);
         if (prefabSucces == true)
                Debug.Log("Prefab was saved successfully");
        else
                Debug.Log("Prefab failed to save" + prefabSucces);

        // string filePath = Path.Combine(Application.dataPath, "TEST_01.fbx");
        // Debug.Log(filePath);
        // ModelExporter.ExportObject(filePath, Testobjects);
    }
}
