using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPortal : MonoBehaviour
{
    bool _triggered;
    public Portal[] portals;

    private void OnTriggerEnter(Collider other)
    {
        if (!_triggered)
        {
            _triggered = true;
            if (other.gameObject.layer == Layers.player.Index)
            {
                for (int i = 0; i < portals.Length; i++)
                {
                    portals[i].SpwanEnemies();
                }
            }
        }
    }
}
