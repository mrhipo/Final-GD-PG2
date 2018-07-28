using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public List<EnemyStats> enemyType;
    public int amountToSpwan;
    public float spawnRate;
    public float spawnDelay;

    List<EnemyStats> _enemiesSpwaned;
    Effects _effects;
    bool _triggered;
    int _count;

	// Use this for initialization
	void Start ()
    {
        enemyType = new List<EnemyStats>();
        _enemiesSpwaned = new List<EnemyStats>();
        _effects = GetComponentInChildren<Effects>();
        _effects.gameObject.SetActive(false);

        GlobalEvent.Instance.AddEventHandler<EnemyKilledEvent>(OnKilledEnemy);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SpwanEnemies()
    {        
        if (!_triggered)
        {
            _triggered = true;
            _effects.gameObject.SetActive(true);
            SoundManager.instance.PlayFX("Portal Activated");

            FrameUtil.RepeatAction(spawnRate, amountToSpwan, Spawn);
        }
    }

    void Spawn()
    {
        //Enemies _enemy = //Instanciar enemigo.
       //_enemiesSpwaned.Add(_enemy);
    }

    void ClosePortal()
    {
        _effects.gameObject.SetActive(false);
        SoundManager.instance.StopFX("Portal Activated");
        //Save Game
    }

    void OnKilledEnemy(EnemyKilledEvent enemyKilledEvent)
    {
        if (_enemiesSpwaned.Contains(enemyKilledEvent.enemy))
        {
            _count++;
            if (_count >= amountToSpwan)
            {
                GlobalEvent.Instance.Dispatch(new PortalClosedEvent());
                ClosePortal();
                _count = 0;
            }
        }
    }

}
