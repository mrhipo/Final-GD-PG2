using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour {
    
    public Action<Damage> OnTakeDamage = delegate { };

}
