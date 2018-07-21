using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSkillEvent : GameEvent
{
    public string skillName;
    public Color skillColor;
    public GameObject skillObj;

    public NewSkillEvent(string skillName, Color skillColor, GameObject skillObj)
    {
        this.skillName = skillName;
        this.skillColor = skillColor;
        this.skillObj = skillObj;
    }
}
