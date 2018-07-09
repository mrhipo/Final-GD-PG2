using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

[CustomEditor(typeof(WallWaypoints))]
public class EditorWallWaypoints : Editor {

    WallWaypoints myTarget;

    bool[] pos = new bool[3] { false, false, true };
   
    float extraRot = 90;
    string roomName = "The Name";


    void OnEnable()
    {
        myTarget = (WallWaypoints)target;
        Tools.hidden = true;
    }

    void OnDisable()
    {
        Tools.hidden = false;
    }

    public override void OnInspectorGUI ()
    {
        base.OnInspectorGUI ();

        if (GUILayout.Button ("Add Waypoint"))
        {
            myTarget.waypoints.Add(new Vector3(myTarget.transform.position.x + 4, myTarget.transform.position.y, myTarget.transform.position.z+1));
        }

       

        //posGroupEnabled = EditorGUILayout.BeginToggleGroup("Toggle", posGroupEnabled);
        pos[0] = EditorGUILayout.Toggle("x", pos[0]);
        pos[1] = EditorGUILayout.Toggle("y", pos[1]);
        pos[2] = EditorGUILayout.Toggle("z", pos[2]);
        //EditorGUILayout.EndToggleGroup();

        extraRot = EditorGUILayout.FloatField("ExtraRot",extraRot);
        roomName = EditorGUILayout.TextField("Room Name",roomName);

        if (GUILayout.Button ("GenerateWalls"))
        {
            GenerateWalls ();
            GenerateFloor ();
        }

        if (GUILayout.Button ("Remove Walls"))
        {
            myTarget.walls.Clear ();
            DestroyImmediate (myTarget.room);
        }

        if (GUILayout.Button ("Remove Vectors"))
        {
            myTarget.waypoints.Clear ();
        }



    }

    void GenerateWalls(){
        float size = 0;

        if(pos[0])
            size = myTarget.basicWall.GetComponentInChildren<Renderer>().bounds.size.x;
        if (pos [1])
            size = myTarget.basicWall.GetComponentInChildren<Renderer> ().bounds.size.y;
        if(pos[2])
            size = myTarget.basicWall.GetComponentInChildren<Renderer>().bounds.size.z;


        myTarget.room = new GameObject(roomName);
        Vector3 cohesion = Vector3.zero;
        for (int i = 0; i < myTarget.waypoints.Count; i++) {
            cohesion += myTarget.waypoints[i];
        }
        cohesion /= myTarget.waypoints.Count;
        myTarget.room.transform.position = cohesion;

        foreach (var item in myTarget.waypoints)
        {
            var newColumn = Instantiate((GameObject)Resources.Load("Column"));
            newColumn.transform.position = item;
            myTarget.walls.Add(newColumn);
            newColumn.transform.SetParent(myTarget.room.transform, true);
        }

        for (int i = 0; i < myTarget.waypoints.Count; i++)
        {
           
            var crr = myTarget.waypoints[i];
            var next = myTarget.waypoints[(i + 1) % myTarget.waypoints.Count];
            Vector3 deltaPos = next - crr;
            var normal = Vector3.Cross(deltaPos, Vector3.up);
            float magnitude = deltaPos.magnitude;
            Vector3 direction = deltaPos / magnitude;
            var count = magnitude / size;


            for (int j = 0; j < count; j++)
            {

                var newWall = Instantiate(myTarget.basicWall);
                newWall.transform.position = crr + direction * j * size;
                newWall.transform.rotation = Quaternion.LookRotation (normal);
                newWall.transform.eulerAngles += Vector3.up * extraRot;
                newWall.transform.SetParent (myTarget.room.transform, true);
                myTarget.walls.Add(newWall);
            }
        }
    }

    void GenerateFloor ()
    {

        Vector2[] vertices2D = myTarget.waypoints.Select (a => new Vector2 (a.x, a.z)).ToArray();

        // Use the triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(vertices2D);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i=0; i<vertices.Length; i++) {
            vertices[i] = new Vector3(vertices2D[i].x, 0, vertices2D[i].y);
        }

        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        // Set up game object with mesh;
        myTarget.floor = new GameObject("Floor");
        myTarget.floor.tag = "Floor";
        myTarget.floor.AddComponent(typeof(MeshRenderer));
        //Toma el mat de las paredes
        myTarget.floor.GetComponent<MeshRenderer>().material = myTarget.basicWall.GetComponentInChildren<MeshRenderer>().sharedMaterial;
        MeshFilter filter = myTarget.floor.AddComponent(typeof(MeshFilter)) as MeshFilter;
        filter.mesh = msh;
        var pos = myTarget.floor.transform.position;
        pos.y = 0;
        myTarget.floor.transform.position = pos;
        myTarget.floor.GetComponent<Renderer>().material = myTarget.floorMaterial;


        myTarget.floor.transform.SetParent (myTarget.room.transform, true);
        myTarget.floor.AddComponent<MeshCollider>();
       

    }

    public static Action OnSceneGuiUpdate = delegate { };
    public static Action<Vector3> OnClick = delegate { };
  

    public void OnSceneGUI(){

        Handles.BeginGUI ();

        for (int i = 0; i < myTarget.waypoints.Count; i++) {
            Handles.Label (myTarget.waypoints [i],"("+i+")");
        }

        for (int i = 0; i < myTarget.waypoints.Count; i++) {
            var pos = Handles.PositionHandle (myTarget.waypoints [i], Quaternion.identity);
            pos.y = 0;
            myTarget.waypoints[i] = pos;
        }

        Handles.EndGUI ();

        OnSceneGuiUpdate();


    }

    public static void CheckMouse()
    {
        Event e = Event.current;
        int controlID = GUIUtility.GetControlID(FocusType.Passive);
        switch (e.GetTypeForControl(controlID))
        {
            case EventType.MouseDown:
                GUIUtility.hotControl = controlID;
                RaycastHit hit;
               // EditorWallWaypoints.OnSceneGuiUpdate -= EditorWallWaypoints.CheckMouse;
                Ray r = HandleUtility.GUIPointToWorldRay(e.mousePosition);
                if (Physics.Raycast(r, out hit))
                {
                    OnClick(hit.point);
                }
                e.Use();
                break;
        }
    }

}


public class Triangulator
{
    private List<Vector2> vertexs = new List<Vector2>();

    public Triangulator (Vector2[] points) {
        vertexs = new List<Vector2>(points);
    }

    public int[] Triangulate() {
        List<int> indices = new List<int>();

        int n = vertexs.Count;
        if (n < 3)
            return indices.ToArray();

        int[] V = new int[n];
        if (Area() > 0) {
            for (int v = 0; v < n; v++)
                V[v] = v;
        }
        else {
            for (int v = 0; v < n; v++)
                V[v] = (n - 1) - v;
        }

        int nv = n;
        int count = 2 * nv;
        for (int m = 0, v = nv - 1; nv > 2; ) {
            if ((count--) <= 0)
                return indices.ToArray();

            int u = v;
            if (nv <= u)
                u = 0;
            v = u + 1;
            if (nv <= v)
                v = 0;
            int w = v + 1;
            if (nv <= w)
                w = 0;

            if (Snip(u, v, w, nv, V)) {
                int a, b, c, s, t;
                a = V[u];
                b = V[v];
                c = V[w];
                indices.Add(a);
                indices.Add(b);
                indices.Add(c);
                m++;
                for (s = v, t = v + 1; t < nv; s++, t++)
                    V[s] = V[t];
                nv--;
                count = 2 * nv;
            }
        }

        indices.Reverse();
        return indices.ToArray();
    }

    private float Area () {
        int vertexCount = vertexs.Count;
        float area = 0.0f;
        for (int p = vertexCount - 1, q = 0; q < vertexCount; p = q++) {
            Vector2 pval = vertexs[p];
            Vector2 qval = vertexs[q];
            area += pval.x * qval.y - qval.x * pval.y;
        }
        return (area * 0.5f);
    }

    private bool Snip (int u, int v, int w, int n, int[] V) {
        int p;
        Vector2 A = vertexs[V[u]];
        Vector2 B = vertexs[V[v]];
        Vector2 C = vertexs[V[w]];
        if (Mathf.Epsilon > (((B.x - A.x) * (C.y - A.y)) - ((B.y - A.y) * (C.x - A.x))))
            return false;
        for (p = 0; p < n; p++) {
            if ((p == u) || (p == v) || (p == w))
                continue;
            Vector2 P = vertexs[V[p]];
            if (InsideTriangle(A, B, C, P))
                return false;
        }
        return true;
    }

    private bool InsideTriangle (Vector2 A, Vector2 B, Vector2 C, Vector2 P) {
        float ax, ay, bx, by, cx, cy, apx, apy, bpx, bpy, cpx, cpy;
        float cCROSSap, bCROSScp, aCROSSbp;

        ax = C.x - B.x; ay = C.y - B.y;
        bx = A.x - C.x; by = A.y - C.y;
        cx = B.x - A.x; cy = B.y - A.y;
        apx = P.x - A.x; apy = P.y - A.y;
        bpx = P.x - B.x; bpy = P.y - B.y;
        cpx = P.x - C.x; cpy = P.y - C.y;

        aCROSSbp = ax * bpy - ay * bpx;
        cCROSSap = cx * apy - cy * apx;
        bCROSScp = bx * cpy - by * cpx;

        return ((aCROSSbp >= 0.0f) && (bCROSScp >= 0.0f) && (cCROSSap >= 0.0f));
    }
}
