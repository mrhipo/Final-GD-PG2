﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchRotation : MonoBehaviour, IUpdate
{
    public int angle;

    public void Start()
    {
        UpdateManager.instance.AddUpdate(this);
    }

    void IUpdate.Update()
    {
        transform.Rotate(Vector3.up, angle);
        transform.Rotate(Vector3.right, angle);
    }

    private void OnDestroy()
    {
        UpdateManager.instance.RemoveUpdate(this);
    }
}
