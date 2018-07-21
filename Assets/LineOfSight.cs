using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour , ILineOfSight
{

    public GameObject Target{ get; set;  }

    private void OnTriggerEnter(Collider other)
    {
        Target = other.gameObject;
    }
    
}

public interface ILineOfSight
{
    GameObject Target
    {
        get;
        set;
    }
}
