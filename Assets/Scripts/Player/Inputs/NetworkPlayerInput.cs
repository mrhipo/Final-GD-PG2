using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerInput : PlayerInput {
    public override float Vertical { get { return Input.GetAxis("Vertical"); } }
    public override float Horizontal { get { return Input.GetAxis("Horizontal"); } }
    public override float RotationX { get { return Input.GetAxis("Mouse X"); } }
    public override float RotationY { get { return Input.GetAxis("Mouse Y") * Time.deltaTime; } }
    public override bool Aiming { get { return Input.GetKey(KeyCode.Mouse1); } }
    public override bool Shooting { get { return Input.GetKey(KeyCode.Mouse0); } }
}