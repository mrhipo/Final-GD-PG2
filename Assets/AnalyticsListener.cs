using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class AnalyticsListener : MonoBehaviour
{

    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindObjectOfType<PlayerStats>();
        GlobalEvent.Instance.AddEventHandler<PlayerDeadEvent>(OnPlayerDead);
        GlobalEvent.Instance.AddEventHandler<LevelCompleteTimedEvent>(OnLevelCompleted);
        GlobalEvent.Instance.AddEventHandler<FireBallCasted>(OnFireBallSpell);
        GlobalEvent.Instance.AddEventHandler<ShieldBallCasted>(OnShieldBallCasted);
        GlobalEvent.Instance.AddEventHandler<FreezeBallCasted>(OnFreezeBallCasted);
        GlobalEvent.Instance.AddEventHandler<VoltBallCasted>(OnVoltBallCasted);
        GlobalEvent.Instance.AddEventHandler<StatUpgrade>(OnStatUpgrade);
        GlobalEvent.Instance.AddEventHandler<SpellUpgrade>(OnSpellUpgrade);

    }

    private void OnSpellUpgrade(SpellUpgrade s)
    {
        Analytics.CustomEvent("Spell Upgrade", new Dictionary<string, object>
        {
            { "Spell Type", s.type }         ,
            { "scene", SceneManager.GetActiveScene().name }
        });
    }

    private void OnStatUpgrade( StatUpgrade s)
    {
        Analytics.CustomEvent("Stat Upgrade", new Dictionary<string, object>
        {
            { "Stat Type", s.type }         ,
            { "scene", SceneManager.GetActiveScene().name }
        });
    }

    private void OnVoltBallCasted()
    {
        Analytics.CustomEvent("Volt Casted", new Dictionary<string, object>
        {
            { "position", playerStats.transform.position }         ,
            { "scene", SceneManager.GetActiveScene().name }
        });
    }

    private void OnFreezeBallCasted()
    {
        Analytics.CustomEvent("Freeze Casted", new Dictionary<string, object>
        {
            { "position", playerStats.transform.position }         ,
            { "scene", SceneManager.GetActiveScene().name }
        });
    }

    private void OnShieldBallCasted()
    {
        Analytics.CustomEvent("Shield Casted", new Dictionary<string, object>
        {
            { "position", playerStats.transform.position }         ,
            { "scene", SceneManager.GetActiveScene().name }
        });
    }

    private void OnFireBallSpell()
    {
        Analytics.CustomEvent("Fire Casted", new Dictionary<string, object>
        {
            { "position", playerStats.transform.position }         ,
            { "scene", SceneManager.GetActiveScene().name }
        });
    }

    private void OnLevelCompleted(LevelCompleteTimedEvent t)
    {
        Analytics.CustomEvent("LevelComplete", new Dictionary<string, object>
        {
            { "time", t.time },
            { "Level", SceneManager.GetActiveScene().name }
        });
    }

    private void OnPlayerDead()
    {
        Analytics.CustomEvent("PlayerDead", new Dictionary<string, object>
        {
            { "potions", playerStats.transform.position },
            { "Level", SceneManager.GetActiveScene().name }
        });
    }

    
}


public class FireBallCasted : GameEvent { }
public class FreezeBallCasted : GameEvent { }
public class ShieldBallCasted : GameEvent { }
public class VoltBallCasted : GameEvent { }