using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Controller : SaveObject
{
    public GameObject loading;
    int _count;

    private void Start()
    {
        base.Start();
        GlobalEvent.Instance.Dispatch(new LevelStartEvent("Shut down the barriers.                 Barriers deactivated: " + _count + " / 4"));
        GlobalEvent.Instance.AddEventHandler<BarrierOffEvent>(OnBarrierOff);
    }

    private void OnBarrierOff()
    {
        _count++;
        UpdateUI();
    }

    private void UpdateUI()
    {
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

    public override void Save()
    {
        SaveData(Key, JsonUtility.ToJson(new IntMemento { value = _count }));
    }

    public override void Load()
    {
        int count = GetValue<IntMemento>().value;
        _count = count;
        UpdateUI();
    }

    [System.Serializable]
    public class IntMemento
    {
        public int value;
    }
}
