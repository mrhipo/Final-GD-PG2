using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class GruntSpecial : SpecialAttackBehaviour
{
	public float attackRange;
	public float prewarmCastDashDelay;
    public float dashTime;

    public float speedMultiplier;
    public MeleeAttackBehaviour meleDashDamage;

	//Cabeceada para testeo
	public override bool Condition
	{
		get {
            var d = Vector3.Distance(transform.position, enemyStats.target.position);
            return d <= attackRange && d >= enemyStats.agent.stoppingDistance;	
		}
	}

    public override void ExecuteSpecialAttack()
    {
	    enemyStats.agent.isStopped = true;
		FrameUtil.AfterDelay(prewarmCastDashDelay, () => Dash());
    }

	private void Dash()
	{
        enemyStats.agent.isStopped = false;
        var dashPosition = enemyStats.TargetPosition;
		enemyStats.agent.speed = enemyStats.speed * speedMultiplier;
        meleDashDamage.Attack(0,dashTime);
        enemyStats.agent.SetDestination(dashPosition);
		FrameUtil.AfterDelay(dashTime, () => FinishAttack());
	}

	private void FinishAttack()
	{
        SpecialFinish();
        enemyStats.agent.speed = enemyStats.speed;
    }
}



public class EnumFlagsAttribute: PropertyAttribute
{
	public EnumFlagsAttribute() { }
}