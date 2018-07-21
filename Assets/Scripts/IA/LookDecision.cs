using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return Look(controller);
    }

    private bool Look(StateController controller)
    {
        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.green);

        var target = controller.Target;
        if (target == null) return false;

        var deltaPos = target.transform.position - controller.transform.position;
        var rayCast = Physics.Raycast(controller.eyes.position, deltaPos, controller.enemyStats.lookRange, Layers.player.Mask);

        return  rayCast;
    }

}
