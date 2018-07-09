using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class UpdateManager: MonoBehaviour
{
    private List<IUpdate> toUpdate = new List<IUpdate>();

    public static UpdateManager instance { get { return _instance; } }
    private static UpdateManager _instance;

    public void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void AddUpdate(IUpdate updateObj)
    {
        toUpdate.Add(updateObj);
    }

    public void RemoveUpdate(IUpdate updateObj)
    {
        toUpdate.Remove(updateObj);
    }

    public void Update()
    {
        if (!GameManager.instance.onPause)
        {
            for (int i = 0; i < toUpdate.Count; i++)
            {
                toUpdate[i].Update();
            }
        }
    }
}