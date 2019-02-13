﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Controller : MonoBehaviour
{
    public GameObject loading;
    int _count;

    private void Start()
    {
        GlobalEvent.Instance.Dispatch(new LevelStartEvent("Shut down the barriers.                 Barriers deactivated: " + _count + " / 4"));
        GlobalEvent.Instance.AddEventHandler<BarrierOffEvent>(OnBarrierOff);
    }

    private void OnBarrierOff()
    {
        _count++;
        FindObjectOfType<HudEventHandler>().missionObjetive.text = "Shut down the barriers.                 Barriers deactivated: " + _count + " / 4";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.player.Index)
        {
            loading.SetActive(true);
            GlobalEvent.Instance.Dispatch(new LevelCompletedEvent { level = 2 });
            SceneManager.LoadScene(4);
        }
    }
}
