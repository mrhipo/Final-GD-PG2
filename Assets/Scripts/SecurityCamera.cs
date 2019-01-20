using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public LifeObject lifeObject;


    // Start is called before the first frame update
    void Start()
    {
        lifeObject = GetComponent<LifeObject>();

        lifeObject.OnDead += OnDead;
        lifeObject.OnTakeDamage += OnTakeDamage;
    }

    private void OnTakeDamage(Damage obj)
    {
        OnDead();
    }

    private void OnDead()
    {
        GlobalEvent.Instance.Dispatch(new CameraDestroyedEvent());
    }


}
