using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour ,IUpdate
{
    public LifeObject lifeObject;

    public RangeValue mp;
    public float mpRecovery;

    public float increntByLevel = 1.1f;
    public int currentHpLevel;
    public int currentMpLevel;

    //MP EVENT
    public Action OnMpChange = delegate { };

    private void Start()
    {
        currentHpLevel = PlayerPrefs.GetInt("LevelStats-HP", 1);
        currentMpLevel = PlayerPrefs.GetInt("LevelStats-MP", 1);

        UpdateManager.instance.AddUpdate(this);
        lifeObject.OnDead += OnDead;
        lifeObject.OnTakeDamage += OnTakeDamage;
        Set_HP_Level(currentHpLevel);
        Set_MP_Level(currentMpLevel);
    }

    private void Set_HP_Level(int level)
    {
        var multiplier = Mathf.Pow(increntByLevel, level); 
        lifeObject.hp.maxValue *= multiplier;
    }

    private void Set_MP_Level(int level)
    {
        var multiplier = Mathf.Pow(increntByLevel, level);
        mpRecovery *= multiplier;
    }

    private void OnTakeDamage(Damage obj)
    {
        GlobalEvent.Instance.Dispatch(new PlayerTakeDamageEvent());
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

