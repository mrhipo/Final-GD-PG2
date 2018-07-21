using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/CoolDownSpecialAttackDecision")]
public class CoolDownDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.animator.GetCurrentAnimatorStateInfo(1).IsName("SpecialAttack");
    }
}
