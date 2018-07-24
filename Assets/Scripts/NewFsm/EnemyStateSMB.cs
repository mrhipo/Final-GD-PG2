using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyStateSMB : StateMachineBehaviour
{

    public static Dictionary<EnemyState, Type> parState = new Dictionary<EnemyState, Type>();
    static EnemyStateSMB()
    {
       /* parState.Add(EnemyState.ChaseState, typeof(ChaseState));
        parState.Add(EnemyState.PatrolState, typeof(PatrolState));
        parState.Add(EnemyState.Attack, typeof(AttackState));
        parState.Add(EnemyState.TargetLost, typeof(TargetLost));*/
    }

    MonoBehaviour monobehaviour;
    public EnemyState changeTo;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        monobehaviour = monobehaviour ?? (animator.GetComponentInChildren(parState[changeTo], true) as MonoBehaviour);
        if (monobehaviour)
            monobehaviour.enabled = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (monobehaviour)
            monobehaviour.enabled = false;
    }

}
public enum EnemyState
{
    ChaseState,
    PatrolState,
    Attack,
    TargetLost,
}