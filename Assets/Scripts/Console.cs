using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
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
}
