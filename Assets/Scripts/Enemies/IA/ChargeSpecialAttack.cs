using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ChargeSpecialAttack")]
public class ChargeSpecialAttack : ActionBase
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    protected void Attack(StateController controller)
    {
        controller.SpecialAttack();
        Debug.Log("Not Implemented yet");
    }
}
