using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkRandomPosition : MonoBehaviour
{
    public Vector3 RandomPos()
    {
        var p = Vector3.zero;
        var l = new List<Transform>();
        foreach (Transform item in transform)
            l.Add(item);
        return l[Random.Range(0, l.Count)].position; 
    }
}
