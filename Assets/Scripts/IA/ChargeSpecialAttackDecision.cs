using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Decisions/SpecialAttackDecision")]
public class ChargeSpecialAttackDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        if (controller.PlayerInSpecialAttackRange && controller.CheckIfCountDownElapsed(CoolDownID.SpecialAttack, controller.enemyStats.specialAttackRate))
            return true;

        return false;
    }
}
