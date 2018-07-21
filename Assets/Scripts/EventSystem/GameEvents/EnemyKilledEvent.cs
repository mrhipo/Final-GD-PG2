using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKilledEvent : GameEvent
{
    GameObject _enemy; //cambiar GO por la clase enemies generica

	public EnemyKilledEvent(GameObject enemy)
    {
        _enemy = enemy;
    }
}
