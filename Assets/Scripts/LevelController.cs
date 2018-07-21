using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        GlobalEvent.Instance.AddEventHandler<EnemyKilledEvent>(OnKilledEnemy);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnKilledEnemy(GameEvent enemyKilledEvent)
    {
      
    }
}
