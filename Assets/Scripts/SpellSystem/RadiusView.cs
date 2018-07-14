﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusView : MonoBehaviour
{

    public float radius;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
