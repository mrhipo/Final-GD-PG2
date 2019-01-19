using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour ,IUpdate
{
    public LifeObject lifeObject;
    public RangeValue mp;
    public float mpRecovery;
    public float speed;

    public float damage = 1;
    public float attackSpeed = .5f;

    public Action OnMpChange = delegate { };

    private void Start()
    {
        InitStatsLevel();

        GlobalEvent.Instance.AddEventHandler<StatUpgrade>(OnStatUpgraded);

        UpdateManager.instance.AddUpdate(this);

        lifeObject.OnDead += OnDead;
        lifeObject.OnTakeDamage += OnTakeDamage;
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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GlobalEvent.Instance.Dispatch(new StatUpgrade { type = StatType.Hp });
            GlobalEvent.Instance.Dispatch(new StatUpgrade { type = StatType.Mp });
            GlobalEvent.Instance.Dispatch(new StatUpgrade { type = StatType.MpRecovery });
            GlobalEvent.Instance.Dispatch(new StatUpgrade { type = StatType.Speed });
        }
    }

    private void OnStatUpgraded(StatUpgrade gameData)
    {
        AddLevel(gameData.type);
    }


    public float HealthPercentage()
    {
        return lifeObject.hp.Percentage;
    }

    public float ManaPercentage()
    {
        return mp.Percentage;
    }


    #region "StatsIncremente by Level"
    [Header("Level Stats")]
    [Tooltip("For each level the stats will increment by this percentage.")]
    public float increntByLevel = 0.1f;
    private int currentHpLevel;
    private int currentMpLevel;
    private int currentMpRecoveryLevel;
    private int currentSpeedLevel;

    private float initialHp;
    private float initialMp;
    private float initialMpRecovery;
    private float initialSpeed;

    public void InitStatsLevel()
    {
        //TODO delete this when the game has to save the stats progress.
        ResetStats();

        initialHp = lifeObject.hp.maxValue;
        initialMp = mp.maxValue;
        initialMpRecovery = mpRecovery;
        initialSpeed = speed;

        currentHpLevel = PlayerPrefs.GetInt("LevelStats-HP", 0);
        currentMpLevel = PlayerPrefs.GetInt("LevelStats-MP", 0);
        currentMpRecoveryLevel = PlayerPrefs.GetInt("LevelStats-MP-Recovery", 0);
        currentSpeedLevel = PlayerPrefs.GetInt("LevelStats-SPEED", 0);

        //Debug.Log($"Levels:  hp:{currentHpLevel} mp:{currentMpLevel} MPrecovery:{currentMpRecoveryLevel} speed:{currentSpeedLevel}");

        Set_HP_Level(currentHpLevel);
        Set_MP_Level(currentMpLevel);
        Set_MP_Recovery_Level(currentMpRecoveryLevel);
        Set_Speed_Level(currentSpeedLevel);

        
    }

    private void ResetStats()
    {
        PlayerPrefs.SetInt("LevelStats-HP", 0);
        PlayerPrefs.SetInt("LevelStats-MP", 0);
        PlayerPrefs.SetInt("LevelStats-MP-Recovery", 0);
        PlayerPrefs.SetInt("LevelStats-SPEED", 0);
    }

    private void Set_HP_Level(int level)
    {
        lifeObject.hp.maxValue = initialHp + initialHp * increntByLevel * level;
    }

    private void Set_MP_Level(int level)
    {
        mp.maxValue = initialMp + initialMp * increntByLevel * level;
    }

    private void Set_Speed_Level(int level)
    {
        GetComponent<PlayerController>().speed = initialSpeed + initialSpeed * increntByLevel * level;
    }

    private void Set_MP_Recovery_Level(int level)
    {
        mpRecovery = initialMpRecovery + initialMpRecovery * increntByLevel * level;
    }

    public void AddLevel(StatType type)
    {
        switch (type)
        {
            case StatType.Hp:
                PlayerPrefs.SetInt("LevelStats-HP", ++currentHpLevel);
                Set_HP_Level(currentHpLevel);
                break;
            case StatType.Mp:
                PlayerPrefs.SetInt("LevelStats-MP", ++currentMpLevel);
                Set_MP_Level(currentMpLevel);
                break;
            case StatType.MpRecovery:
                PlayerPrefs.SetInt("LevelStats-MP-Recovery", ++currentMpRecoveryLevel);
                Set_MP_Recovery_Level(currentMpRecoveryLevel);
                break;
            case StatType.Speed:
                PlayerPrefs.SetInt("LevelStats-SPEED", ++currentSpeedLevel);
                Set_Speed_Level(currentSpeedLevel);
                break;
        }
    }

    #endregion
}

public enum StatType
{
    Hp,Mp,MpRecovery,Speed
}

