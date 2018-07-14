using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public LifeObject lifeObject;

    public RangeValue mp;
    public float mpRecovery;

    public float damage;
    public float attackSpeed;

    public float magicPower = 1;

    //MP EVENT
    public Action<float> OnMpChange = delegate { };
  
    public void ConsumeMp(float amount)
    {
        mp.CurrentValue -= amount;
        OnMpChange(mp.Percentage);
    }

    public void RecoverMp(float amount)
    {
        mp.CurrentValue += amount;
        OnMpChange(mp.Percentage);
    }

}

