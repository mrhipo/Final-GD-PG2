using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilledEvent : GameEvent
{
    public Enemies enemy; 

	public EnemyKilledEvent(Enemies enemy)
    {
        this.enemy = enemy;
    }
}
