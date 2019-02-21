using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : SaveObject
{
    bool _triggered;
    public GameObject barrier;
    public GameObject bright;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.player.Index && !_triggered)
        {
            _triggered = true;
            barrier.SetActive(false);
            bright.SetActive(false);
            SoundManager.instance.PlayFX("Console Activated");
            GlobalEvent.Instance.Dispatch(new BarrierOffEvent());
        }
    }

    public override void Save()
    {
        SaveData(Key, JsonUtility.ToJson(new BooleanMemento { boolean = _triggered }));
    }

    public override void Load()
    {
        var trigerred = GetValue<BooleanMemento>().boolean;
        if (trigerred)
        {
            _triggered = true;
            barrier.SetActive(false);
            bright.SetActive(false);
        }
    }
}
