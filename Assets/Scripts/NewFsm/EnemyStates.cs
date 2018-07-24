using System;
using System.Reflection;

using UnityEngine;

[Flags]
public enum EnemyStates {
	Idle          = 1<<0 ,
	Attack        = 1<<1 ,
	Chase         = 1<<2 ,
	Dead          = 1<<3 , 
	SpecialAttack = 1<<4 ,
	Patrol        = 1<<5 
} 

