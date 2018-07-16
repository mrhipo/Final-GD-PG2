using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layers : MonoBehaviour {

    public static LayerMask enemies;
    public static LayerMask player;

    static Layers()
    {
        enemies = 1 << LayerMask.NameToLayer("Enemy");
        player = 1 << LayerMask.NameToLayer("Player");
    }

}
