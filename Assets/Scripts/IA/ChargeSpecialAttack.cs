using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/ChargeSpecialAttack")]
public class ChargeSpecialAttack : ActionBase
{
    public override void Act(StateController controller)
    {
        Attack();
    }

    protected void Attack()
    {
        Debug.Log("Not Implemented yet");
    }
}
