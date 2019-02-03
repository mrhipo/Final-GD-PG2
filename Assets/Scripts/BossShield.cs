using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour
{
    public int speedRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speedRotation);
    }
}
