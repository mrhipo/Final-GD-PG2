using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Controller : MonoBehaviour
{
    public GameObject loading;
    int _count;

    private void Start()
    {
        GlobalEvent.Instance.Dispatch(new LevelStartEvent("Shut down the barriers.                 Barriers deactivated: " + _count + " / 3"));
        GlobalEvent.Instance.AddEventHandler<BarrierOffEvent>(OnBarrierOff);
    }

    private void OnBarrierOff()
    {
        _count++;
        FindObjectOfType<HudEventHandler>().missionObjetive.text = "Shut down the barriers.                 Barriers deactivated: " + _count + " / 3";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.player.Index)
        {
            loading.SetActive(true);
            //Cargar nivel 3
        }
    }
}
