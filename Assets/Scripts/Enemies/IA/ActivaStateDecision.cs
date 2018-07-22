using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/ActivaState")]
public class ActivaStateDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.lineOfSight.Target.activeSelf;
    }
}
