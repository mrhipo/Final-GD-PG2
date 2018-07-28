using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAttackBehaviour : MonoBehaviour
{
    [EnumFlagsAttribute]
    public EnemyStates statesCondition;

    public bool canAttack;
    public float cdAttack = 6;

    public abstract bool Condition { get; }

    protected EnemyStats enemyStats;

    public void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    private void Update()
    {
        if (canAttack && (enemyStats.currentState & statesCondition) > 0 && Condition)
        {
            enemyStats.fsm.SetTrigger("SpecialAttack");
            ExecuteSpecialAttack();
            ToggleCanAttack();
        }
    }

    public abstract void ExecuteSpecialAttack();

    protected void SpecialFinish()
    {
        enemyStats.fsm.SetTrigger("SpecialFinish");
    }

    public void ToggleCanAttack()
    {
        canAttack = false;
        FrameUtil.AfterDelay(cdAttack, () => canAttack = true);
    }
}
