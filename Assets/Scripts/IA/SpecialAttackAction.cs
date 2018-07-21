using UnityEngine;

public abstract class SpecialAttackAction : ActionBase
{
    public override void Act(StateController controller)
    {
        if (controller.PlayerInSpecialAttackRange)
            if (controller.CheckIfCountDownElapsed(CoolDownID.SpecialAttack, controller.enemyStats.specialAttackRate))
                SpecialAttack(controller);
    }

    private void SpecialAttack(StateController controller)
    {
        controller.SpecialAttack();
        Attack();
    }

    protected abstract void Attack();
}
