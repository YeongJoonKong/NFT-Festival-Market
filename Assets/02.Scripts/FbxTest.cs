using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using TMPro;

public class FbxTest : MonoBehaviour
{
    public GameObject Testobjects;
    // Start is called before the first frame update
    public void cretefbx()
    {
        TextMeshProUGUI nftText = Testobjects.GetComponentInChildren<TextMeshProUGUI>();
        int nftname = Random.Range(0, 10);
        nftText.text = " " + nftname;
        
        string localPath = "Assets/" + Testobjects.name + ".prefab";
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
        
        bool prefabSucces;
        PrefabUtility.SaveAsPrefabAsset(Testobjects, localPath, out prefabSucces);
         if (prefabSucces == true)
                Debug.Log("Prefab was saved successfully");
            else
                Debug.Log("Prefab failed to save" + prefabSucces);

        // string filePath = Path.Combine(Application.dataPath, "TEST_01.fbx");
        // Debug.Log(filePath);
        // ModelExporter.ExportObject(filePath, Testobjects);
    }
}
