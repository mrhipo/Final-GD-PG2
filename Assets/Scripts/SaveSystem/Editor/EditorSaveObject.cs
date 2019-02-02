using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class EditorSaveObject
{

    [MenuItem("Tools/Generate Save Ids")]
    private static void GenerateAllSaveIds()
    {

        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects( rootObjects );

        int id = 0;
        // iterate root objects and do something
        for (int i = 0; i < rootObjects.Count; ++i)
        {
            GameObject gameObject = rootObjects[ i ];
            var thisSaveObject = gameObject.GetComponent<SaveObject>();
            if(thisSaveObject != null)
            {
                thisSaveObject.id = id++;
                EditorUtility.SetDirty(thisSaveObject);
            }

            var list = gameObject.GetComponentsInChildren<SaveObject>(true);
           
            foreach (var item in list)
            {
                item.id = id++;
                EditorUtility.SetDirty(item);
            }
        }

        
        Debug.Log("FIN SAVE ID");
    }

}