using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour {

    private EnemyStats enemyStats;
	public EnemyStates state;
	
	private bool canAttack = true;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyStats = enemyStats ?? animator.GetComponent<EnemyStats>();
		enemyStats.currentState = state;
		enemyStats.agent.velocity = Vector3.zero;

		enemyStats.animator.SetFloat("Speed",0);

	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (canAttack)
        {
            Debug.Log("Attack");
	        enemyStats.animator.SetTrigger("Attack");
            canAttack = false;
            FrameUtil.AfterDelay(enemyStats.attackRate, () => canAttack = true);
        }

        if(enemyStats.TargetDistance > enemyStats.attackRange)
        {
            animator.SetTrigger("Chase");
        }

	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}
