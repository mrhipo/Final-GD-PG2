using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackBehaviour : AttackBehaviour
{
    public WarlockBullet bulletPrfab;
    public Transform spawnPoint;

    private EnemyStats _enemyStats;

    event Action OnUpdate = delegate { };

    private void Start()
    {
        _enemyStats = GetComponent<EnemyStats>();
    }

    public void Update()
    {
        OnUpdate();
    }

    public override void Attack()
    {
        _enemyStats.Speed = 0;
        _enemyStats.agent.isStopped = true;
        OnUpdate += LookToTarget;

        FrameUtil.AfterDelay(delayToActivateAttack, () =>
        {
            var warlockBullet = Instantiate(bulletPrfab);
            warlockBullet.Initialize(spawnPoint.position, _enemyStats.TargetPosition, _enemyStats.basicDamage);
        });
        
        FrameUtil.AfterDelay(delayToActivateAttack + attackDuration, () =>
        {
            _enemyStats.Speed = _enemyStats.speed;
            _enemyStats.agent.isStopped = false;
            OnUpdate -= LookToTarget;

            _enemyStats.fsm.SetTrigger("Idle");
            Debug.Log("End Magic");
        });
        
    }

    private void LookToTarget()
    {
        transform.forward = Vector3.Lerp(transform.forward, _enemyStats.target.position - transform.position, 1f);
    }
}
