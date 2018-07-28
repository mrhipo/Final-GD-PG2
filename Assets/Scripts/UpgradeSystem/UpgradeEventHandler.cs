using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeEventHandler : MonoBehaviour {

    int maxStatsValue = 7;
    public int currentStatsValue;

    public int noSpellReward = 2;
    public int levelTimeReward = 1;
    public int noDamageReward = 2;
    public int noDeadReward = 1;
    public int levelCompleteReward = 1;

    public float levelTime;

	void Awake () {
        GlobalEvent.Instance.AddEventHandler<FinderCollectEvent>(OnSkillUsed);
        GlobalEvent.Instance.AddEventHandler<FireBallKillEvent>(OnSkillUsed);
        GlobalEvent.Instance.AddEventHandler<VoltKillEvent>(OnSkillUsed);

        GlobalEvent.Instance.AddEventHandler<LevelStartEvent>(OnLevelStart);

        GlobalEvent.Instance.AddEventHandler<LevelCompletedEvent>(OnLevelComplete);

        GlobalEvent.Instance.AddEventHandler<PlayerDeadEvent>(OnPlayerDead);

        GlobalEvent.Instance.AddEventHandler<PlayerTakeDamageEvent>(OnPlayerDamage);

        currentStatsValue = maxStatsValue;
    }

    private void OnPlayerDamage()
    {
        currentStatsValue -= noDamageReward;
        GlobalEvent.Instance.RemoveEventHandler<PlayerTakeDamageEvent>(OnPlayerDamage);
    }

    private void OnPlayerDead()
    {
        currentStatsValue -= noDeadReward;
        GlobalEvent.Instance.RemoveEventHandler<PlayerDeadEvent>(OnPlayerDead);
    }

    private void OnSkillUsed()
    {
        currentStatsValue -= noSpellReward;

        GlobalEvent.Instance.RemoveEventHandler<FinderCollectEvent>(OnSkillUsed);
        GlobalEvent.Instance.RemoveEventHandler<FireBallKillEvent>(OnSkillUsed);
        GlobalEvent.Instance.RemoveEventHandler<VoltKillEvent>(OnSkillUsed);
    }

    float initTime;
    private void OnLevelStart()
    {
        initTime = Time.time;
        GlobalEvent.Instance.RemoveEventHandler<LevelStartEvent>(OnLevelStart);
    }

    private void OnLevelComplete()
    {
        if(Time.time - initTime > levelTime)
            currentStatsValue -= levelTimeReward;

        print("Upgrade Skill " + currentStatsValue);

        GlobalEvent.Instance.RemoveEventHandler<LevelCompletedEvent>(OnLevelComplete);
    }

    void OnDestroy ()
    {
        GlobalEvent.Instance.RemoveEventHandler<FinderCollectEvent>(OnSkillUsed);
        GlobalEvent.Instance.RemoveEventHandler<FireBallKillEvent>(OnSkillUsed);
        GlobalEvent.Instance.RemoveEventHandler<VoltKillEvent>(OnSkillUsed);
        GlobalEvent.Instance.RemoveEventHandler<LevelStartEvent>(OnLevelStart);
        GlobalEvent.Instance.RemoveEventHandler<LevelCompletedEvent>(OnLevelComplete);
        GlobalEvent.Instance.RemoveEventHandler<PlayerDeadEvent>(OnPlayerDead);
        GlobalEvent.Instance.RemoveEventHandler<PlayerTakeDamageEvent>(OnPlayerDamage);
    }

}
