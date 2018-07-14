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

    public float magicPower = 1;

    //-------Events-------------
    //Hp
    public Action OnTakeDamage = delegate { };
    public Action OnDead = delegate { };
    public Action<float> OnLifeChange = delegate { };
    //Mp
    public Action<float> OnMpChange = delegate { };

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void TakeDamage(float damage)
    {
        hp.CurrentValue -= damage;
        OnTakeDamage();
        OnLifeChange(hp.Percentage);
        if (hp.CurrentValue == 0) OnDead();
    }

    public void Heal(float amount)
    {
        hp.CurrentValue += amount;
        OnLifeChange(hp.Percentage);
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


}

