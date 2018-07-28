using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : StateMachineBehaviour
{

    private EnemyStats enemyStats;
    public EnemyStates state;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyStats = enemyStats ?? animator.GetComponent<EnemyStats>();
        enemyStats.animator.SetTrigger("Dead");
        enemyStats.currentState = state;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}
