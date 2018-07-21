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
        RaycastHit hit;

        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.attackRange, Color.red);

        if (Physics.SphereCast(controller.eyes.position, controller.enemyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemyStats.attackRange,Layers.player.Mask))
        {
            if (controller.CheckIfCountDownElapsed(CoolDownID.Attack,controller.enemyStats.attackRate))
            {
                Debug.Log("ATTACK");
            }
        }
    }
}
