using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class GruntSpecial : SpecialAttackBehaviour
{
	public float attackRange;	
	//Cabeceada para testeo
	public override bool Condition
	{
		get {
            return Vector3.Distance(transform.position, enemyStats.target.position) <= attackRange ;	 
		}
	}

    public override void ExecuteSpecialAttack()
    {
	    enemyStats.agent.speed = enemyStats.speed * 2;
	    
	    FrameUtil.AfterDelay(4, () => enemyStats.fsm.SetTrigger("SpecialFinish"));
	    FrameUtil.AfterDelay(4, () => enemyStats.agent.speed = enemyStats.speed);
         print("//TODO MAGIC");
    }
}



public class EnumFlagsAttribute: PropertyAttribute
{
	public EnumFlagsAttribute() { }
}