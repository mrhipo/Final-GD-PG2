using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Freeze : MonoBehaviour, IUpdate
{
    public float duration;

    private float _enemySpeed;
    private float _timer;

    //ToDo: True name.
    //private Enemy owner;

    private EnemyStats _enemy;

    void Start()
    {
        _enemy.agent.velocity = Vector3.zero;
        UpdateManager.instance.AddUpdate(this);
    }

    public void RestartTime() { _timer = 0; }

    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= duration)
        {
            UpdateManager.instance.RemoveUpdate(this);
            Destroy(this);
        }
    }
}
