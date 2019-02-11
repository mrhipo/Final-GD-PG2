using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : MonoBehaviour , ISpeed
{
    public float speedRotation;
    public Collider c;

    public float Speed
    {
        get
        {
            return speedRotation;
        }

        set
        {
            this.speedRotation = speedRotation;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<Collider>();
        GetComponent<HitObject>().OnTakeDamage += OnDamage;
    }

    private void OnDamage(Damage obj)
    {
        if (obj.Owner.GetComponent<FreezeSpell>() != null)
        {
            speedRotation = 5;
            c.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, speedRotation);
    }
}
