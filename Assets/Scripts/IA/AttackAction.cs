using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : ActionBase
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.attackRange, Color.red);

        if (controller.HasTarget && controller.PlayerInAttackRange)
            if (controller.CheckIfCountDownElapsed(CoolDownID.Attack, controller.enemyStats.attackRate))
                controller.Attack();
    }
}
