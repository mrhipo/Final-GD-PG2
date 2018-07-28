using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackBehaviour : AttackBehaviour
{
    public WarlockBullet bulletPrfab;
    public Transform spawnPoint;

    private EnemyStats _enemyStats;

    private void Start()
    {
        _enemyStats = GetComponent<EnemyStats>();
    }

    public override void Attack()
    {
        _enemyStats.Speed = 0;
        _enemyStats.agent.isStopped = true;

        FrameUtil.AfterDelay(delayToActivateAttack, () =>
        {
            var warlockBullet = Instantiate(bulletPrfab);
            warlockBullet.Initialize(spawnPoint.position, _enemyStats.TargetPosition, _enemyStats.damage);
        });
        
        FrameUtil.AfterDelay(delayToActivateAttack + attackDuration, () =>
        {
            _enemyStats.Speed = _enemyStats.speed;
            _enemyStats.agent.isStopped = false;
            
            _enemyStats.fsm.SetTrigger("Idle");
            Debug.Log("End Magic");
        });
        
    }
}
