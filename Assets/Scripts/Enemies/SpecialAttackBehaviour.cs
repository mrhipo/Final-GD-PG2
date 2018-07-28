using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAttackBehaviour : MonoBehaviour
{
    [EnumFlagsAttribute]
    public EnemyStates statesCondition;

    public abstract bool Condition { get; }

    protected EnemyStats enemyStats;

    public void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        if ((enemyStats.currentState & statesCondition) > 0 && Condition)
        {
            enemyStats.fsm.SetTrigger("SpecialAttack");
            ExecuteSpecialAttack();
        }
    }

    public abstract void ExecuteSpecialAttack();
}
