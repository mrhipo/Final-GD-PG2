using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour ,IUpdate
{
    public LifeObject lifeObject;
    public RangeValue mp;
    public float mpRecovery;
    public float speed;

    public float damage = 1;
    public float attackSpeed = .5f;

    public Action OnMpChange = delegate { };

    [Header("Currency")]
    public int credits;
    public int experience;


    private Animator animator;

    bool IsNetworking;

    private void Start()
    {
        InitStatsLevel();

        animator = GetComponent<Animator>();

        GlobalEvent.Instance.AddEventHandler<StatUpgrade>(OnStatUpgraded);
        GlobalEvent.Instance.AddEventHandler<CreditsPickedEvent>(OnCreditsPicked);
        GlobalEvent.Instance.AddEventHandler<ExperiencePickedEvent>(OnExperiencePicked);

        UpdateManager.instance.AddUpdate(this);

        lifeObject.OnDead += OnDead;
        lifeObject.OnTakeDamage += OnTakeDamage;

    }

    private void OnExperiencePicked(ExperiencePickedEvent experience)
    {
        this.experience += experience.amount;

    }

    private void OnCreditsPicked(CreditsPickedEvent credits)
    {
        this.credits += credits.amount;
    }

    private void OnTakeDamage(Damage obj)
    {
        GlobalEvent.Instance.Dispatch(new PlayerTakeDamageEvent());
    }

    private void OnDead()
    {
        if(!IsNetworking)
            GlobalEvent.Instance.Dispatch(new PlayerDeadEvent());
        animator.SetTrigger("Dead");
        if (!IsNetworking)
            StartCoroutine(WaitThenReload());
    }

    private IEnumerator WaitThenReload()
    {
        yield return new WaitForSeconds(3);
        SaveGameManager.loadGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    public float increntStatsByLevel = 0.1f;
    [Tooltip("For each level the cost will increment by this percentage.")]
    public float increntCreditCostByLevel = .75f;


    private int currentHpLevel;
    private int currentMpLevel;
    private int currentMpRecoveryLevel;
    private int currentSpeedLevel;

    private float initialHp;
    private float initialMp;
    private float initialMpRecovery;
    private float initialSpeed;

    [Tooltip("Base Cost For Stats & Spells.")]
    public float costUpgradeStatsSpells;

    public void InitStatsLevel()
    {
        //TODO delete this when the game has to save the stats progress.
        //ResetStats();

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
        lifeObject.hp.maxValue = initialHp + initialHp * increntStatsByLevel * level;
    }

    private void Set_MP_Level(int level)
    {
        mp.maxValue = initialMp + initialMp * increntStatsByLevel * level;
    }

    private void Set_Speed_Level(int level)
    {
        if(GetComponent<PlayerController>()!=null)
            GetComponent<PlayerController>().speed = initialSpeed + initialSpeed * increntStatsByLevel * level;
    }

    private void Set_MP_Recovery_Level(int level)
    {
        mpRecovery = initialMpRecovery + initialMpRecovery * increntStatsByLevel * level;
    }

    public void AddLevel(StatType type)
    {
        switch (type)
        {
            case StatType.Hp:
                if (GetCostUpgrade(currentHpLevel) > credits) return;
                DiscountCredits(currentHpLevel);
                PlayerPrefs.SetInt("LevelStats-HP", ++currentHpLevel);
                Set_HP_Level(currentHpLevel);
                break;
            case StatType.Mp:
                if (GetCostUpgrade(currentMpLevel) > credits) return;
                DiscountCredits(currentMpLevel);
                PlayerPrefs.SetInt("LevelStats-MP", ++currentMpLevel);
                Set_MP_Level(currentMpLevel);
                break;
            case StatType.MpRecovery:
                if (GetCostUpgrade(currentMpRecoveryLevel) > credits) return;
                DiscountCredits(currentMpRecoveryLevel);
                PlayerPrefs.SetInt("LevelStats-MP-Recovery", ++currentMpRecoveryLevel);
                Set_MP_Recovery_Level(currentMpRecoveryLevel);
                break;
            case StatType.Speed:
                if (GetCostUpgrade(currentSpeedLevel) > credits) return;
                DiscountCredits(currentSpeedLevel);
                PlayerPrefs.SetInt("LevelStats-SPEED", ++currentSpeedLevel);
                Set_Speed_Level(currentSpeedLevel);
                break;
        }
    }

    public void DiscountCredits(int level)
    {
        credits -= GetCostUpgrade(level);
    }

    public int GetCostUpgrade(int level)
    {
        return (int)(costUpgradeStatsSpells + costUpgradeStatsSpells * increntCreditCostByLevel * level);
    }
    #endregion
}

public enum StatType
{
    Hp,Mp,MpRecovery,Speed
}

