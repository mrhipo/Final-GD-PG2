using UnityEngine;

public class PlayerInput {

    public virtual float Vertical { get { return Input.GetAxis("Vertical"); } }
    public virtual float Horizontal { get { return Input.GetAxis("Horizontal"); } }
    public virtual float RotationX { get { return Input.GetAxis("Mouse X"); } }
    public virtual float RotationY { get { return Input.GetAxis("Mouse Y") * Time.deltaTime; } }
    public virtual bool Aiming { get { return Input.GetKey(KeyCode.Mouse1); } }
    public virtual bool Shooting { get { return Input.GetKey(KeyCode.Mouse0); } }
    public virtual bool Sk1 { get { return Input.GetKey(KeyCode.Alpha1); } }
    public virtual bool Sk2 { get { return Input.GetKey(KeyCode.Alpha2); } }
    public virtual bool Sk3 { get { return Input.GetKey(KeyCode.Alpha3); } }
    public virtual bool Sk4 { get { return Input.GetKey(KeyCode.Alpha4); } }

}
