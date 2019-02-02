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

    ISpeed speeder;
    Animator animator;

    void Start()
    {
        speeder = GetComponent<ISpeed>();
        _enemySpeed = speeder.Speed;
        speeder.Speed = 0;
        UpdateManager.instance.AddUpdate(this);
        var enemyStats = GetComponent<EnemyStats>();
        if (enemyStats != null)
            animator = enemyStats.animator;
        else
            animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    public void RestartTime() { _timer = 0; }

    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= duration)
        {
            UpdateManager.instance.RemoveUpdate(this);
            Destroy(this);
            speeder.Speed = _enemySpeed;
            animator.enabled = true;
        }
    }
}
