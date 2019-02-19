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
    protected bool triggered;
    int _count;

    public Door[] doors;

    // Use this for initialization
    void Start()
    {
        _effects = GetComponentInChildren<Effects>();
        _effects.gameObject.SetActive(false);
    }


    public void SpwanEnemies()
    {
        if (!triggered)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].LockDoor();
            }


            triggered = true;
            _effects.gameObject.SetActive(true);
            SoundManager.instance.PlayFX("Portal Activated");

            FrameUtil.RepeatAction(spawnRate, amountToSpwan, Spawn);
        }
    }

    Vector3 offset = Vector3.up * .2f;
    public virtual void Spawn()
    {
        var enemyStats = Instantiate(enemyType[UnityEngine.Random.Range(0, enemyType.Count)]).GetComponent<EnemyStats>();
        enemyStats.lifeObject.OnDead += OnKilledEnemy;
        enemyStats.lifeObject.OnDead += () =>
        {
            enemyStats.lifeObject.OnDead -= OnKilledEnemy;
        };
        enemyStats.agent.Warp(transform.position + offset);
        enemyStats.OnSpawn();
    }

    void ClosePortal()
    {
        _effects.gameObject.SetActive(false);
        SoundManager.instance.StopFX("Portal Activated");

        OpenDoors();
    }

    void OpenDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].UnLockDoor();
        }
    }

    public virtual void OnKilledEnemy()
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
