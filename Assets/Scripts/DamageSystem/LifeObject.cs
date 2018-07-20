using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeObject : HitObject {

    public List<HitObject> hitObjects;

    public RangeValue hp;

    public Action OnDead = delegate { };
    public Action OnLifeChange = delegate { };

    private void Start()
    {
        Active();
    }

    public void Takedamage(float amount)
    {
        hp.CurrentValue -= amount;
        OnLifeChange();
        if (hp.CurrentValue == 0)
            OnDead();
    }

    public void Heal(float amount)
    {
        hp.CurrentValue += amount;
        OnLifeChange();
    }

    private void OnTakeDamage_(Damage damage)
    {
        OnTakeDamage(damage);
        Takedamage(damage.Amount);
    }

    private void Active()
    {
        OnTakeDamage += OnTakeDamage_;
        foreach (var item in hitObjects)
            item.OnTakeDamage += OnTakeDamage_;
    }

    private void Desactive()
    {
        foreach (var item in hitObjects)
            item.OnTakeDamage -= OnTakeDamage_;
    }

    public void SetActive(bool active)
    {
        if (active)
            Active();
        else
            Desactive();
    }

   
}
