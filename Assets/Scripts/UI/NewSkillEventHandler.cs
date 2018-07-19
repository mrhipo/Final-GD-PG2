using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewSkillEventHandler : MonoBehaviour
{
    GameObject obj;
    public GameObject newSkillUI;

	void Start ()
    {
        GlobalEvent.Instance.AddEventHandler<NewSkillEvent>(OnSkillAcquired);
	}

    private void OnSkillAcquired(NewSkillEvent skillEvent)
    {
        obj = skillEvent.skillObj;
        obj.SetActive(true);
        newSkillUI.SetActive(true);
        var text = newSkillUI.GetComponentInChildren<Text>();
        text.text = "New Skill Acquired: " + skillEvent.skillName;
        text.GetComponentInChildren<Outline>().effectColor = skillEvent.skillColor;
        SoundManager.instance.PlayFX("New Skill");
        //CoroutineManager.Instance.RunCoroutine(Clear());
        FrameUtil.AfterDelay(5, Clear);
    }

    void Clear()
    {
        newSkillUI.SetActive(false);
    }
}
