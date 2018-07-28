using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class GruntSpecial : SpecialAttackBehaviour
{
	public float attackRange;
	public float castDashDelay;
	
	private Vector3 _dashPosition;
	
	//Cabeceada para testeo
	public override bool Condition
	{
		get {
			return Vector3.Distance(transform.position, enemyStats.target.position) <= attackRange &&
			       Vector3.Distance(transform.position, _dashPosition) >= enemyStats.agent.stoppingDistance;	
		}
	}

    public override void ExecuteSpecialAttack()
    {
	    print("//TODO MAGIC");
	    enemyStats.agent.isStopped = true;
		FrameUtil.AfterDelay(castDashDelay, () => Dash());
    }

	private void Dash()
	{
		_dashPosition = enemyStats.TargetPosition;
		enemyStats.agent.speed = enemyStats.speed * 2;

		enemyStats.agent.SetDestination(_dashPosition);
		enemyStats.agent.isStopped = false;
	    
		SpecialFinish();
		
		FrameUtil.AfterDelay(4, () => FinishAttack());
	}

	private void FinishAttack()
	{
		enemyStats.agent.speed = enemyStats.speed;
		Debug.Log("End Magic");
	}
}



public class EnumFlagsAttribute: PropertyAttribute
{
	public EnumFlagsAttribute() { }
}