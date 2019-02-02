using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Linq;

public class UpdateManager: MonoBehaviour
{
    private HashSet<IUpdate> toUpdate = new HashSet<IUpdate>();
    private List<IUpdate> ToRunFor = new List<IUpdate>();

    public static UpdateManager instance { get { return _instance; } }
    private static UpdateManager _instance;

    public bool pause;

    public void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void AddUpdate(IUpdate updateObj)
    {
        toUpdate.Add(updateObj);
        ToRunFor = toUpdate.ToList();
    }

    public void RemoveUpdate(IUpdate updateObj)
    {
        toUpdate.Remove(updateObj);
        ToRunFor = toUpdate.ToList();
    }

    public void Update()
    {
        if (!pause)
        {
            
            for (int i = 0; i < ToRunFor.Count; i++)
            {
                ToRunFor[i].Update();
            }
        }
    }
}