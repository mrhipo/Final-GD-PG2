using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsPickedEvent : GameEvent
{
    public int amount;

    public CreditsPickedEvent(int amount)
    {
        this.amount = amount;
    }
}
