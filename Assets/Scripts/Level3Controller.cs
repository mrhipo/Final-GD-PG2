﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Controller : MonoBehaviour
{
    public GameObject barrier;
    public GameObject lifebar;
    public LifeObject dronLifeObject;
    // Start is called before the first frame update
    void Start()
    {
        GlobalEvent.Instance.Dispatch(new LevelStartEvent("Find and destroy the Security Drone"));
        GlobalEvent.Instance.AddEventHandler<DroneDestroyedEvent>(OnDroneDestroyed);

        dronLifeObject.OnDead += OnDroneDestroyed;
    }

    private void OnDroneDestroyed()
    {
        dronLifeObject.OnDead -= OnDroneDestroyed;
        barrier.SetActive(false);
        FindObjectOfType<HudEventHandler>().missionObjetive.text = "Destroy the Main Core";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Layers.player.Index)
        {
            barrier.SetActive(true);
            lifebar.SetActive(true);
            GlobalEvent.Instance.Dispatch(new BossEvent());
        }
    }
}
