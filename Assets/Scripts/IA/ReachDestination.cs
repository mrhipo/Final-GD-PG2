using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ReachDestination")]
public class ReachDestination : Decision
{
    public override bool Decide(StateController controller)
    {
        return  controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance 
                && 
                !controller.navMeshAgent.pathPending;
    }
}
