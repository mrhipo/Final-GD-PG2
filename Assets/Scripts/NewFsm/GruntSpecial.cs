using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GruntSpecial : SpecialAttackBehaviour
{
	public bool active;
	
	public override bool Condition
	{
		get { return (enemyStats.currentState & statesCondition) > 0 && active; }
	}
	
	
}

public abstract class SpecialAttackBehaviour : MonoBehaviour
{
	[EnumFlagsAttribute]
	public EnemyStates statesCondition;
	
	public abstract bool Condition { get; }

	protected EnemyStats enemyStats;

	public void Start()
	{
		enemyStats = GetComponent<EnemyStats>();
	}

	private void Update()
	{
		if (Condition)
		{
			enemyStats.fsm.SetTrigger("SpecialAttack");
		}
	}
}


public class EnumFlagsAttribute: PropertyAttribute
{
	public EnumFlagsAttribute() { }
}