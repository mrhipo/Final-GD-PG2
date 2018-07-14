using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using System;

[ExecuteInEditMode]
public class AssetReplacer : EditorWindow
{
    PropType type;
    Color color;
    WallWaypoints walls;

    [MenuItem("LevelEditor/Asset Replacer")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(AssetReplacer));
    }

    private void OnEnable()
    {
        walls = FindObjectOfType<WallWaypoints>();
    }

    void OnGUI()
    {
        type = (PropType)EditorGUILayout.EnumPopup("Filter:", type);
        var path = "Assets/Resources/" + type.ToString() + "/";

        DirectoryInfo dirInfo = new DirectoryInfo(path);
        FileInfo[] fileInf = dirInfo.GetFiles("*.prefab");

        List<GameObject> allGOs = new List<GameObject>();
        //loop through directory loading the game object and checking if it has the component you want
        foreach (FileInfo fileInfo in fileInf)
        {
            string fullPath = fileInfo.FullName.Replace(@"\", "/");
            string assetPath = "Assets" + fullPath.Replace(Application.dataPath, "");
            GameObject prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;
            allGOs.Add(prefab);
        }

        
        EditorGUILayout.LabelField("Choose an asset to replace");
       
        if (allGOs.Any())
        {
            EditorGUILayout.BeginHorizontal();
            foreach (var prop in allGOs)
            {
                var preview = AssetPreview.GetAssetPreview(prop);
                if (GUILayout.Button(preview, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    GameObject goToDelete = ((GameObject)Selection.activeObject);
                    goToDelete = FindGameObject(goToDelete);

                    Vector3 pos = Selection.activeTransform.position;
                    Quaternion rot = Selection.activeTransform.rotation;
                    Transform root = Selection.activeTransform.parent;
                    walls.walls.Remove((GameObject)Selection.activeObject);
                    DestroyImmediate(Selection.activeObject);
                    var a = GameObject.Instantiate(prop);
                    a.transform.position = pos;
                    a.transform.rotation = rot;
                    a.transform.SetParent(root);
                    walls.walls.Add(a);
                }
                //EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.Separator();        
        EditorGUILayout.LabelField("Emission Color");
        color = EditorGUILayout.ColorField(color);
        if(GUILayout.Button("Set Color"))
        {
            Selection.activeGameObject.GetComponentInChildren<Renderer>().sharedMaterial.SetColor("_EmissionColor", color);
        }
    }

    private GameObject FindGameObject(GameObject go)
    {
        if (go.CompareTag("Root"))
            return go;
        return FindGameObject(go.transform.parent.gameObject);
    }
}
