using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReaperSpecial : SpecialAttackBehaviour
{
    [Header("TimeToExitState")]
    public float finishTime = 3;
    [Header("Distance To Execute Attack")]
    public float minDistance;

    [Header("Time to cast effect")]
    public float timeCastInjure = 1.5f;
    [Header("Range affected")]
    public float rangeAffected = 5;
    [Header("how many time")]
    public float freezeDuration = 3;

    public override bool Condition
    {
        get
        {
            return enemyStats.TargetDistance < minDistance;
        }
    }

    public override void ExecuteSpecialAttack()
    {
        FrameUtil.AfterDelay(timeCastInjure, CastFreezeSpell);
    }


    //TODO Do Feedback!
    private void CastFreezeSpell()
    {
        enemyStats.agent.isStopped = true;
        if (enemyStats.TargetDistance < rangeAffected)
        {
            Freeze freeze = enemyStats.target.GetComponent<Freeze>();

            if (freeze)
                freeze.RestartTime();
            else
            {
                freeze = enemyStats.target.gameObject.AddComponent<Freeze>();
                freeze.duration = freezeDuration;
            }
        }
        FrameUtil.AfterDelay(finishTime, ()=> {
            SpecialFinish();
            enemyStats.agent.isStopped = false;
        });
    }
}
