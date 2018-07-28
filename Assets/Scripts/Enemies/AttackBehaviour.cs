using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBehaviour : MonoBehaviour
{
    public float delayToActivateAttack;
    
    public abstract void Attack();
}