using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using System;

[ExecuteInEditMode]
public class PropPlacer : EditorWindow
{
    AssetType type;
    Color color;
    WallWaypoints walls;

    bool randomRot;

    [MenuItem("LevelEditor/Prop Placer")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PropPlacer));
    }

    private void OnEnable()
    {
        walls = FindObjectOfType<WallWaypoints>();
    }

    private void OnDisable()
    {
        EditorWallWaypoints.OnClick -= OnClick;
        EditorWallWaypoints.OnSceneGuiUpdate -= EditorWallWaypoints.CheckMouse;
    }

    private void OnClick(Vector3 obj)
    {
        var go = GameObject.Instantiate(goToPlace);
        go.transform.position = obj;

        if(randomRot)
            go.transform.eulerAngles += Vector3.up * 360 * UnityEngine.Random.value;

        EditorWallWaypoints.OnClick -= OnClick;
        EditorWallWaypoints.OnSceneGuiUpdate -= EditorWallWaypoints.CheckMouse;
        CreatePop(goToPlace);
        //EditorWallWaypoints.OnClick -= OnClick;
    }

    GameObject goToPlace;

    void OnGUI()
    {
        type = (AssetType)EditorGUILayout.EnumPopup("Filter:", type);
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


        EditorGUILayout.LabelField("Choose an asset to place");

        if (allGOs.Any())
        {
            EditorGUILayout.BeginHorizontal();
            foreach (var prop in allGOs)
            {
                var p = prop;
                var preview = AssetPreview.GetAssetPreview(prop);
                if (GUILayout.Button(preview, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    CreatePop(p);
                }
            }
            EditorGUILayout.EndHorizontal();

            randomRot = EditorGUILayout.Toggle("Random Rotation", randomRot);
        }

        EditorGUILayout.LabelField("Asset to Place");
        var assetPreview = AssetPreview.GetAssetPreview(goToPlace);
        GUI.DrawTexture(GUILayoutUtility.GetRect(100, 100, 100, 100), assetPreview, ScaleMode.ScaleToFit);
    }


    private void CreatePop(GameObject go)
    {
        EditorWallWaypoints.OnClick = delegate { };
        EditorWallWaypoints.OnSceneGuiUpdate = delegate { };
        goToPlace = go;
        EditorWallWaypoints.OnClick += OnClick;
        EditorWallWaypoints.OnSceneGuiUpdate += EditorWallWaypoints.CheckMouse;
    }
    private void OnInspectorUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 8))
            {
                Debug.Log("Floor");
            }
        }
    }
}
