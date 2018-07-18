using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/Scan")]
public class ScaneDecision : Decision
{
    public override bool Decide(StateController controller)
    {

        return Scan(controller);
    }

    private bool Scan(StateController controller)
    {
        controller.navMeshAgent.isStopped = true;
        controller.transform.Rotate(0f, controller.enemyStats.searchingTurnSpeed * Time.deltaTime, 0f);
        return controller.CheckIfCountDownElapsed(controller.enemyStats.searchDuration);

    }
}
