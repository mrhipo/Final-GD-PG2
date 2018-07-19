using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPortal : MonoBehaviour
{
    public Portal[] portals;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.player.Index)
        {
            for (int i = 0; i < portals.Length; i++)
            {
                portals[i].SpwanEnemies();
            }
        }
    }
}
