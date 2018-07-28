using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GruntSpecial : SpecialAttackBehaviour
{
	//Cabeceada para testeo
	public override bool Condition
	{
		get {
            return (enemyStats.currentState & statesCondition) > 0;	 
		}
	}

    public override void ExecuteSpecialAttack()
    {
         print("//TODO MAGIC");
    }
}



public class EnumFlagsAttribute: PropertyAttribute
{
	public EnumFlagsAttribute() { }
}