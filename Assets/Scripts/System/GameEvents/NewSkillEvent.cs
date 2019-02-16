using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSkillEvent : GameEvent
{
    public SpellType type;
    public Color skillColor;
    public GameObject skillObj;

    public NewSkillEvent(SpellType type, Color skillColor, GameObject skillObj)
    {
        this.type = type;
        this.skillColor = skillColor;
        this.skillObj = skillObj;
    }
}
