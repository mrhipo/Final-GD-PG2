using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public List<GameObject> enemyType;
    public int amountToSpwan;
    public float spawnRate;
    public float spawnDelay;

    Effects _effects;
    bool _triggered;
    int _count;

	// Use this for initialization
	void Start ()
    {
        enemyType = new List<GameObject>();
        _effects = GetComponentInChildren<Effects>();
        _effects.gameObject.SetActive(false);
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

    Vector3 offset = Vector3.up * .2f;
    void Spawn()
    {
        var enemyStats = Instantiate(enemyType[UnityEngine.Random.Range(0, enemyType.Count)]).GetComponent<EnemyStats>();
        enemyStats.lifeObject.OnDead += OnKilledEnemy;
    }

    void ClosePortal()
    {
        _effects.gameObject.SetActive(false);
        SoundManager.instance.StopFX("Portal Activated");
    }

    void OnKilledEnemy()
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
