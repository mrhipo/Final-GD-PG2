using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/StunSpecialAttack")]
public class StunSpecialAttack : SpecialAttackAction
{
    protected override void Attack()
    {
        Debug.Log("Stun Special Attack");
    }
}
