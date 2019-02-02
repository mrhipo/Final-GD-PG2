using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class SaveObject : MonoBehaviour
{

    public int id;

    public virtual void Start()
    {
        GlobalEvent.Instance.AddEventHandler<SaveGameEvent>(CheckSaveGame);
        GlobalEvent.Instance.AddEventHandler<LoadGameEvent>(Load);
    }

    private void CheckSaveGame()
    {
        if (SaveGameManager.nameSaved.Contains(Key))
        {
            Debug.LogError("ERROR!. Ya se guardo un Objecto con el mismo nombre. " + Key + " " + gameObject.name);
        }
        else
        {
            SaveGameManager.nameSaved.Add(Key);
        }
    }

    public abstract void Save();

    public abstract void Load();

    protected void SaveData(int key, string value)
    {
        SaveGameManager.pokePref.Add(key, value);
    }


    public void OnDestroy()
    {
        GlobalEvent.Instance.RemoveEventHandler<SaveGameEvent>(Save);
        GlobalEvent.Instance.RemoveEventHandler<LoadGameEvent>(Load);
    }

    public int Key
    {
        get { return id; }
    }

    public T GetValue<T>()
    {
        return JsonUtility.FromJson<T>(SaveGameManager.pokePref[Key]);
    }

    public string GetJson()
    {
        return SaveGameManager.pokePref[Key];
    }

}

internal class LoadGameEvent : GameEvent
{
}