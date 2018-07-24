using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Chase : StateMachineBehaviour {

    private EnemyStats enemyStats;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyStats = enemyStats ?? animator.GetComponent<EnemyStats>();
    }

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		Debug.Log(enemyStats.TargetDistance);
        if(enemyStats.TargetDistance < enemyStats.attackRange)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            enemyStats.agent.SetDestination(enemyStats.TargetPosition);
	        enemyStats.animator.SetFloat("Speed", enemyStats.agent.speed);
        }
    }

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		enemyStats.animator.SetFloat("Speed", 0);
	}

}
