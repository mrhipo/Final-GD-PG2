using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewSkillEventHandler : MonoBehaviour
{
    GameObject obj;
	void Start ()
    {
        GlobalEvent.Instance.AddEventHandler<NewSkillEvent>(OnSkillAcquired);
	}

    private void OnSkillAcquired(NewSkillEvent skillEvent)
    {
        obj = skillEvent.skillObj;
        obj.SetActive(true);
        var text = skillEvent.skillObj.GetComponentInChildren<Text>();
        text.text = "New Skill Acquired: " + skillEvent.skillName;
        text.GetComponentInChildren<Outline>().effectColor = skillEvent.skillColor;
        SoundManager.instance.PlayFX("New Skill");
        //CoroutineManager.Instance.RunCoroutine(Clear());
        FrameUtil.AfterDelay(5, Clear);
    }

    void Clear()
    {
        obj.SetActive(false);
    }
}
