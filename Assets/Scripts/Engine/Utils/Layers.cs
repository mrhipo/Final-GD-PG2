using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers : MonoBehaviour {

    public static LayerMask enemies;

    static Layers()
    {
        enemies = 1 << LayerMask.NameToLayer("Enemy");
    }

}
