using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GruntSpecial : SpecialAttackBehaviour
{
	//Cabeceada para testeo
	public bool active;
	
	//Cabeceada para testeo
	public override bool Condition
	{
		get { var b = (enemyStats.currentState & statesCondition) > 0 && active;
			if (b)
			{
				//Cabeceada para testeo
				active = false;
				//Cabeceada para testeo
				FrameUtil.AfterDelay(8, () => active = true);
				//Cabeceada para testeo
				FrameUtil.AfterDelay(4, () => enemyStats.fsm.SetTrigger("SpecialFinish"));
			}
			return b;
		}
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