using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPortal : SaveObject
{
    bool _triggered;
    public Portal[] portals;

    private void OnTriggerEnter(Collider other)
    {
       if (other.gameObject.layer == Layers.player.Index && !_triggered)
        {
            _triggered = true;
            for (int i = 0; i < portals.Length; i++)
            {
                portals[i].SpwanEnemies();
            }
        } 
    }

    public override void Load()
    {
        var booleanMemento = GetValue<BooleanMemento>();
        if (booleanMemento != null)
        {
            _triggered = booleanMemento.boolean;
        }
    }

    public override void Save()
    {
        SaveData(Key, JsonUtility.ToJson(new BooleanMemento { boolean = _triggered }));
    }
}
