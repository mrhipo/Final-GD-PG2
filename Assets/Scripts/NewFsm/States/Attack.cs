using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour {

    private EnemyStats enemyStats;

    bool canAttack;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyStats = enemyStats ?? animator.GetComponent<EnemyStats>();
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (canAttack)
        {
            Debug.Log("Attack");
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
