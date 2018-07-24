using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : StateMachineBehaviour {

    private EnemyStats enemyStats;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyStats = enemyStats ?? animator.GetComponent<EnemyStats>();
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(Vector3.Distance(enemyStats.TargetPosition, enemyStats.Position) < enemyStats.attackRange)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            enemyStats.agent.SetDestination(enemyStats.TargetPosition);
        }
    }

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}
