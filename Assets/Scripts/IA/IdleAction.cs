using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Idle")]
public class IdleAction : ActionBase
{
    public override void Act(StateController controller)
    {
        Idlear(controller);
    }

    private void Idlear(StateController controller)
    {
        controller.Idle();
    }
}
