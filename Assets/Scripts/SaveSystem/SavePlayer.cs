using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SavePlayer : SaveObject
{
    PlayerStats stats;
    SpellCaster caster;
    AchievementsEventHandler achievements;

    public void Awake()
    {
        stats = GetComponent<PlayerStats>();
        caster = GetComponent<SpellCaster>();
        achievements = FindObjectOfType<AchievementsEventHandler>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
            SaveGameManager.SaveGame("checkpoints");
        if (Input.GetKeyDown(KeyCode.F2))
            SaveGameManager.LoadGame("checkpoints");
    }
    public override void Load()
    {
        var playerm = GetValue<PlayerMemento>();
        Load(playerm);
    }

    public void Load(PlayerMemento playerm,bool overridePosition = true)
    {
        if (playerm != null)
        {
            if(overridePosition)
                stats.GetComponent<NavMeshAgent>().Warp(playerm.position);
            stats.credits = playerm.credits;
            stats.experience = playerm.experience;
            if (playerm.fire)
                GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)0, Color.black, null));
            if (playerm.freeze)
                GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)1, Color.black, null));
            if (playerm.volt)
                GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)2, Color.black, null));
            if (playerm.shield)
                GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)3, Color.black, null));
            stats.currentHpLevel = playerm.currentHpLevel;
            stats.currentMpLevel = playerm.currentMpLevel;
            stats.currentMpRecoveryLevel = playerm.currentMpRecoveryLevel;
            stats.currentSpeedLevel = playerm.currentSpeedLevel;
            stats.fireLevel = playerm.fireLevel;
            stats.freezeLevel = playerm.freezeLevel;
            stats.voltLevel = playerm.voltLevel;
            if (playerm.parrillero)
                achievements.achievements.MakeComplete(AchievementType.Parrillero);
            if (playerm.flash)
                achievements.achievements.MakeComplete(AchievementType.Flash);
            if (playerm.godMode)
                achievements.achievements.MakeComplete(AchievementType.GodMode);
            if (playerm.finder)
                achievements.achievements.MakeComplete(AchievementType.Finder);
            if (playerm.shellShock)
                achievements.achievements.MakeComplete(AchievementType.ShellShock);

            stats.RefreshStats();
        }
    }

    public override void Save()
    {
        SaveData(Key, JsonUtility.ToJson(GetPlayerMemento()));
    }

    public PlayerMemento GetPlayerMemento()
    {
        return new PlayerMemento
        {
            position = stats.transform.position,
            credits = stats.credits,
            experience = stats.experience,
            fire = !caster.IsBlocked(SpellType.Fire),
            freeze = !caster.IsBlocked(SpellType.Freeze),
            volt = !caster.IsBlocked(SpellType.Volt),
            shield = !caster.IsBlocked(SpellType.Shield),
            currentHpLevel = stats.currentHpLevel,
            currentMpLevel = stats.currentMpLevel,
            currentMpRecoveryLevel = stats.currentMpRecoveryLevel,
            currentSpeedLevel = stats.currentSpeedLevel,
            fireLevel = stats.fireLevel,
            freezeLevel = stats.freezeLevel,
            voltLevel = stats.voltLevel,
            parrillero = achievements.achievements.CheckComplete(AchievementType.Parrillero),
            flash = achievements.achievements.CheckComplete(AchievementType.Flash),
            godMode = achievements.achievements.CheckComplete(AchievementType.GodMode),
            finder = achievements.achievements.CheckComplete(AchievementType.Finder),
            shellShock = achievements.achievements.CheckComplete(AchievementType.ShellShock)
        };
    }
}


public class PlayerMemento
{
    public Vector3 position;
    public int credits;
    public int experience;
    //Spells
    public bool fire;
    public bool freeze;
    public bool volt;
    public bool shield;
    //achievemetns
    public bool parrillero;
    public bool flash;
    public bool godMode;
    public bool finder;
    public bool shellShock;
    //Stats Level
    public int currentHpLevel;
    public int currentMpLevel;
    public int currentMpRecoveryLevel;
    public int currentSpeedLevel;
    //Spell Level
    public int fireLevel;
    public int freezeLevel;
    public int voltLevel;
}