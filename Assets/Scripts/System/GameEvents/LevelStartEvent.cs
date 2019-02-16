using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartEvent : GameEvent
{
    public string objetive;

    public LevelStartEvent(string objetive)
    {
        this.objetive = objetive;
    }
}
