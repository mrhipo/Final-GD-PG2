using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class AchievementsEventHandler : MonoBehaviour
{
	public AchievementsFactory achievements;
	GlobalEvent ge = new GlobalEvent();

    [HideInInspector]
	public int finder;
	[HideInInspector]
	public bool playerDead;
	
    /// <summary>
    /// Max time in second for win The Flash Achievement.
    /// </summary>
	public float limitTime;
	
	float startTime;
	
	void Start()
	{
		GlobalEvent.Instance.AddEventHandler<FinderCollectEvent>(OnFinderCollect);
		
		GlobalEvent.Instance.AddEventHandler<FireBallKillEvent>(OnFireballKill);
	
		GlobalEvent.Instance.AddEventHandler<VoltKillEvent>(OnVoltKill);

		GlobalEvent.Instance.AddEventHandler<PlayerDeadEvent>(OnPlayerDead);
		
		GlobalEvent.Instance.AddEventHandler<LevelStartEvent>(OnLevelStart);

		GlobalEvent.Instance.AddEventHandler<LevelCompletedEvent>(OnGameEndedGodModeAchievement);

        GlobalEvent.Instance.AddEventHandler<LevelCompletedEvent>(OnLevelCompleteTimer);


        achievements.Init();
	}

	void OnDestroy()
	{
		GlobalEvent.Instance.RemoveEventHandler<FinderCollectEvent>(OnFinderCollect);
		
		GlobalEvent.Instance.RemoveEventHandler<FireBallKillEvent>(OnFireballKill);
	
		GlobalEvent.Instance.RemoveEventHandler<VoltKillEvent>(OnVoltKill);

		GlobalEvent.Instance.RemoveEventHandler<PlayerDeadEvent>(OnPlayerDead);
		
		GlobalEvent.Instance.RemoveEventHandler<LevelStartEvent>(OnLevelStart);

		GlobalEvent.Instance.RemoveEventHandler<LevelCompletedEvent>(OnGameEndedGodModeAchievement);

		GlobalEvent.Instance.RemoveEventHandler<LevelCompletedEvent>(OnLevelCompleteTimer);
		
		achievements.Remove();

	}

	private void OnLevelStart(LevelStartEvent obj)
	{
		startTime = Time.time;
		GlobalEvent.Instance.AddEventHandler<LevelCompletedEvent>(OnLevelCompleteTimer);
	}

	private void OnLevelCompleteTimer()
	{
        GlobalEvent.Instance.Dispatch<LevelCompleteTimedEvent>(new LevelCompleteTimedEvent { time = Time.time - startTime });

        if (Time.time - startTime < limitTime)
			GlobalEvent.Instance.Dispatch<AchievementCompleteEvent>(new AchievementCompleteEvent{type = AchievementType.Flash});
	}

	private static void OnVoltKill(VoltKillEvent voltKillEvent)
	{
		if (voltKillEvent.killed >= 3)
			GlobalEvent.Instance.Dispatch<AchievementCompleteEvent>(new AchievementCompleteEvent{type = AchievementType.ShellShock});
	}

	private static void OnFireballKill(FireBallKillEvent fireBallEvent)
	{
		if (fireBallEvent.killed >= 3)
			GlobalEvent.Instance.Dispatch<AchievementCompleteEvent>(new AchievementCompleteEvent{type = AchievementType.Parrillero});
	}

	private void OnFinderCollect()
	{
		finder++;
		if (finder == 3)
			GlobalEvent.Instance.Dispatch<AchievementCompleteEvent>(new AchievementCompleteEvent{type = AchievementType.Finder});
	}

	private void OnGameEndedGodModeAchievement(LevelCompletedEvent gameEvent)
	{
		if (gameEvent.level == 3 && !playerDead)
			GlobalEvent.Instance.Dispatch<AchievementCompleteEvent>(new AchievementCompleteEvent{type = AchievementType.GodMode});
	}
	
	private void OnPlayerDead()
	{
		playerDead = true;
	}
	
	public void ResetAchievements()
	{
		playerDead = false;
		finder = 0;
	}
}


public class LevelCompleteTimedEvent : GameEvent
{
    public float time;
}

