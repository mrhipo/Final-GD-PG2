using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPopUp : MonoBehaviour
{
	public Text text;
	public Image image;

	public void SetAchievement(Achievement achievement)
	{
		text.text = achievement.name;
		image.sprite = achievement.sprite;
	}

}
