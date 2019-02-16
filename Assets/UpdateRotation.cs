﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRotation : MonoBehaviour
{
    public float speed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, speed);
    }
}
