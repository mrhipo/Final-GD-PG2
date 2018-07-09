using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWaypoints : MonoBehaviour
{
    public GameObject basicWall;
    public Material floorMaterial;
    [HideInInspector]
    public List<GameObject> walls = new List<GameObject>();
    [HideInInspector]
    public List<Vector3> waypoints = new List<Vector3>();
    [HideInInspector]
    public GameObject room;
    [HideInInspector]
    public GameObject floor;
}
