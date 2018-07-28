using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage {

    GameObject owner;
    float amount;

    public Damage(GameObject owner, float amount)
    {
        this.owner = owner;
        this.amount = amount;
    }

    public GameObject Owner { get { return owner; } }
    public float Amount { get { return amount; } set { amount = value; } }

}
