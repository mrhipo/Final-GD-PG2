using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IUpdate
{
    public static GameManager instance { get { return _instance; } }
    private static GameManager _instance;

    [HideInInspector]
    public bool onPause;
        
    public void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    public void Update()
    {
        
    }

    public void OnPause()
    {
        onPause = !onPause;
    }
}

