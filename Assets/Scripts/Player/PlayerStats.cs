using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public RangeValue hp;

    public RangeValue mp;
    public float mpRecovery;

    public float damage;
    public float attackSpeed;

    //** Events
    //Hp
    public Action OnTakeDamage = delegate { };
    public Action OnDead = delegate { };
    public Action OnLifeChange = delegate { };
    //Mp
    public Action OnMpChange = delegate { };

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void TakeDamage(float damage)
    {
        hp.CurrentValue -= damage;
        OnTakeDamage();
        OnLifeChange();
        if (hp.CurrentValue == 0) OnDead();
    }

    public void Heal(float amount)
    {
        hp.CurrentValue += amount;
        OnLifeChange();
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


}

