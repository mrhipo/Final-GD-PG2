using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement-xxxx",menuName = "Achievement/CreateAchievement")]
public class Achievement : ScriptableObject
{
	//constant
	public AchievementType type;
	public Sprite sprite;
	public string text;
	public bool completed;
	
	public bool CheckComplete(AchievementType t)
	{
		return t == type;
	}
	
}

public enum AchievementType{
	GodMode,
	Finder,
	Parrillero,
	ShellShock,
	Flash
}

public class AchievementCompleteEvent:GameEvent
{
	public AchievementType type;
}