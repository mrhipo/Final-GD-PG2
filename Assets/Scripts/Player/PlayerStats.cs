using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour ,IUpdate
{
    public LifeObject lifeObject;

    public RangeValue mp;
    public float mpRecovery;

    public float damage;
    public float attackSpeed;

    public float magicPower = 1;

    //MP EVENT
    public Action<float> OnMpChange = delegate { };

    private void Start()
    {
        UpdateManager.instance.AddUpdate(this);
    }

    private void Destroy()
    {
        UpdateManager.instance.RemoveUpdate(this);
    }

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

    void IUpdate.Update()
    {
        mp.CurrentValue += Time.deltaTime * mpRecovery;
    }
}

