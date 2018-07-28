using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBehaviour : AttackBehaviour
{
    public GameObject sphereCollider;
    public float attackDuration;
    
    public override void Attack()
    {
        FrameUtil.AfterDelay(delayToActivateAttack, () => sphereCollider.SetActive(true));
        FrameUtil.AfterDelay(delayToActivateAttack + attackDuration, () => sphereCollider.SetActive(false));
    }
    
}
