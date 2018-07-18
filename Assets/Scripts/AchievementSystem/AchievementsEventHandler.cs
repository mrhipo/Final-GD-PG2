using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class AchievementsEventHandler : MonoBehaviour
{
	public AchievementsFactory achievements;
	GlobalEvent ge = new GlobalEvent();

	public int finder;
	public bool playerDead;
	
	void Start()
	{
		GlobalEvent.Instance.AddEventHandler<FinderCollectEvent>(OnFinderCollect);
		GlobalEvent.Instance.AddEventHandler<FireBallKillEvent>(OnFireballKill);
	
		GlobalEvent.Instance.AddEventHandler<VoltKillEvent>(OnVoltKill);

		GlobalEvent.Instance.AddEventHandler<PlayerDeadEvent>(OnPlayerDead);
		GlobalEvent.Instance.AddEventHandler<LevelCompletedEvent>(OnLevelComplete);
		
		achievements.Init();
		
	}

	private void OnVoltKill(VoltKillEvent voltKillEvent)
	{
		if (voltKillEvent.killed >= 3)
			GlobalEvent.Instance.Dispatch<AchievementCompleteEvent>(new AchievementCompleteEvent{type = AchievementType.ShellShock});
	}

	private void OnFireballKill(FireBallKillEvent fireBallEvent)
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

	private void OnLevelComplete(LevelCompletedEvent gameEvent)
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


