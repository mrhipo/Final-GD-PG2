using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayer : SaveObject
{
    PlayerStats stats;
    SpellCaster caster;
    public void Awake()
    {
        stats = GetComponent<PlayerStats>();
        caster = GetComponent<SpellCaster>();
    }

    public override void Load()
    {
        var playerm = GetValue<PlayerMemento>();
        if (playerm != null)
        {
            stats.transform.position = playerm.position;
            stats.credits = playerm.credits;
            stats.experience = playerm.experience;
            if(playerm.fire)
                GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)0, Color.black, null));
            if (playerm.freeze)
                GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)1, Color.black, null));
            if (playerm.volt)
                GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)2, Color.black, null));
            if (playerm.shield)
                GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)3, Color.black, null));
        }    
    }        

    public override void Save()
    {
        SaveData(Key, JsonUtility.ToJson(new PlayerMemento {
                                                    position = stats.transform.position,
                                                    credits = stats.credits,
                                                    experience = stats.experience,
            fire = !caster.IsBlocked(SpellType.Fire),
            freeze = !caster.IsBlocked(SpellType.Freeze),
            volt = !caster.IsBlocked(SpellType.Volt),
            shield = !caster.IsBlocked(SpellType.Shield),
        }));
    }
}


public class PlayerMemento
{
    public Vector3 position;
    public int credits;
    public int experience;
    public bool fire;
    public bool freeze;
    public bool volt;
    public bool shield;
}