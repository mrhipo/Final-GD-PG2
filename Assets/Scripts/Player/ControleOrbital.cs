
using UnityEngine;
using System.Collections;
using System;

public class ControleOrbital : MonoBehaviour, IUpdate {

    private float vertical;
    private float velcoidadeDeGiro = 4.0f;
    void Start ()
    {
        UpdateManager.instance.AddUpdate(this);
        vertical = transform.eulerAngles.x;
    }
	

    void IUpdate.Update()
    {
        var mouseVertical = Input.GetAxis("Mouse Y");
        vertical = (vertical - velcoidadeDeGiro * mouseVertical) % 360f;
        vertical = Mathf.Clamp(vertical, -30, 60);
        transform.localRotation = Quaternion.AngleAxis(vertical, Vector3.right);
    }
}
