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
        FrameUtil.AfterDelay(delayToActivateAttack, () =>
        {
            var warlockBullet = Instantiate(bulletPrfab);
            warlockBullet.Initialize(spawnPoint.position, _enemyStats.TargetPosition, _enemyStats.damage);
        });
        
    }
}
