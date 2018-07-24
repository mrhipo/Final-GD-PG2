﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateMachineBehaviour {

    private EnemyStats enemyStats;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemyStats = enemyStats ?? animator.GetComponent<EnemyStats>();
		enemyStats.animator.SetTrigger("Idle");
    }

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (enemyStats.TargetDistance < enemyStats.sightDistance)
        {
            animator.SetTrigger("Chase");
        }
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}

}
