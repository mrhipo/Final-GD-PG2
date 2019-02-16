using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilledEvent : GameEvent
{
    public EnemyStats enemy; 

	public EnemyKilledEvent(EnemyStats enemy)
    {
        this.enemy = enemy;
    }
}
