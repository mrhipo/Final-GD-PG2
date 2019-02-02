using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckPoint : SaveObject
{
    Transform player;

    bool _gameSaved;
    float _duration = 1f;
    int _fades = 3;

    void OnTriggerEnter(Collider c)
    {
        if (_gameSaved) return;
        _gameSaved = true;
        SaveGameManager.SaveGame("checkpoints");
    }

    public override void Load()
    {
        var dead = GetValue<BooleanMemento>();
        if (dead != null && dead.boolean)
        {
            gameObject.SetActive(false);
        }
    }

    public override void Save()
    {
        SaveData(Key, JsonUtility.ToJson(new BooleanMemento(_gameSaved)));
    }

}