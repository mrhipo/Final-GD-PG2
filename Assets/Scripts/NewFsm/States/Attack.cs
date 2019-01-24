using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour {

    private EnemyStats enemyStats;
	public EnemyStates state;
	
	private AttackBehaviour _attackBehaviour;
	
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyStats = enemyStats ?? animator.GetComponent<EnemyStats>();
		_attackBehaviour = _attackBehaviour ?? animator.gameObject.GetComponent<AttackBehaviour>();
		
		enemyStats.currentState = state;
		enemyStats.agent.velocity = Vector3.zero;

		enemyStats.animator.SetFloat("Speed",0);

	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (enemyStats.canAttack)
        {
	        enemyStats.animator.SetTrigger("Attack");
            enemyStats.canAttack = false;
            FrameUtil.AfterDelay(enemyStats.attackRate, () => enemyStats.canAttack = true);
	        _attackBehaviour.Attack();
        }
        enemyStats.transform.forward = Vector3.Lerp(enemyStats.transform.forward, enemyStats.TargetDirection, .1f);

        if (enemyStats.TargetDistance > enemyStats.attackRange)
        {
            animator.SetTrigger("Chase");
        }

	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}
