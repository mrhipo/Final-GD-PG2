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
    public Action OnMpChange = delegate { };

    private void Start()
    {
        UpdateManager.instance.AddUpdate(this);
        lifeObject.OnDead += OnDead;
    }

    private void OnDead()
    {
        GlobalEvent.Instance.Dispatch(new PlayerDeadEvent());
    }

    private void Destroy()
    {
        UpdateManager.instance.RemoveUpdate(this);
    }

    public void ConsumeMp(float amount)
    {
        mp.CurrentValue -= amount;
        OnMpChange();
    }

    public void RecoverMp(float amount)
    {
        mp.CurrentValue += amount;
        OnMpChange();
    }

    void IUpdate.Update()
    {
        mp.CurrentValue += Time.deltaTime * mpRecovery;
    }

    public int HealthPercentage()
    {
        return 0;
    }

    public int ManaPercentage()
    {
        return 0;
    }
}

