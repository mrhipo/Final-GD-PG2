using System;
using UnityEngine;
using UnityEngine.UI;

public class NewSkillEventHandler : MonoBehaviour
{
    public GameObject newSkillUI;

    public GameObject fireSpellUI;
    public GameObject freezeSpellUI;
    public GameObject voltSpellUI;
    public GameObject shieldSpellUI;

    void Start ()
    {
        GlobalEvent.Instance.AddEventHandler<NewSkillEvent>(OnSkillAcquired);
	}

    private void OnSkillAcquired(NewSkillEvent skillEvent)
    {
        GameObject obj = GetSkillObject(skillEvent);
        obj.SetActive(true);
        if (skillEvent.skillColor == Color.black) return;
        newSkillUI.SetActive(true);
        var text = newSkillUI.GetComponentInChildren<Text>();
        text.text = "New Skill Acquired: " + skillEvent.type;
        text.GetComponentInChildren<Outline>().effectColor = skillEvent.skillColor;
        SoundManager.instance.PlayFX("New Skill");
        FrameUtil.AfterDelay(5, Clear);
    }

    private GameObject GetSkillObject(NewSkillEvent skillEvent)
    {
        switch (skillEvent.type)
        {
            case SpellType.Fire:
                return fireSpellUI;
            case SpellType.Freeze:
                return freezeSpellUI;
            case SpellType.Volt:
                return voltSpellUI;
            case SpellType.Shield:
                return shieldSpellUI;
            default:
                throw new Exception("NewSkillEventHandler Error. The Sprite for skill " + skillEvent.type + " wasnt set!");
        }
    }

    void Clear()
    {
        newSkillUI.SetActive(false);
    }
}
