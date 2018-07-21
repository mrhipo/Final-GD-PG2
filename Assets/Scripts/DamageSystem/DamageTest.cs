using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour {

    public float amount = 1;
    private void OnTriggerEnter(Collider other)
    {
        LifeObject lo = null;
        if((lo = other.GetComponent<LifeObject>())!= null)
            lo.OnTakeDamage(new Damage(gameObject, amount));
    }
}
