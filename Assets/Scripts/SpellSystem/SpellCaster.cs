using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour, IUpdate
{
    public List<Spell> _availableSpells;
    public Transform spawnPoint;

    private PlayerStats _playersStats;
    private PlayerInputSpell _playerInputController;

    private void Start()
    {
        _playerInputController = new PlayerInputSpell();
        _playersStats = FindObjectOfType<PlayerStats>();

        InitSpell();

        UpdateManager.instance.AddUpdate(this);

        GlobalEvent.Instance.AddEventHandler<SpellUpgrade>(OnSpellUpgraded);
    }

    private void OnSpellUpgraded(SpellUpgrade gameData)
    {
        if (_playersStats.experience >= _playersStats.GetCostUpgrade(GetSpellLevel(gameData.type)))
        {
            _playersStats.experience -= _playersStats.GetCostUpgrade(GetSpellLevel(gameData.type));
            PlayerPrefs.SetInt(gameData.type+"-Level", 1+ PlayerPrefs.GetInt(gameData.type + "-Level", 0));
        }
    }

    private int GetSpellLevel(SpellType type)
    {
        switch(type)
        {
            case SpellType.Fire:
                return PlayerPrefs.GetInt("Fire-Level", 0);
            case SpellType.Freeze:
                return PlayerPrefs.GetInt("Freeze-Level", 0);
            case SpellType.Volt:
                return PlayerPrefs.GetInt("Volt-Level", 0);
            default:
                return 0;
        }
    }

    private void InitSpell()
    {
        //Remove this line if we wanna keep the level
        //ResetSpellsLevels();

        foreach (var item in _availableSpells)
            item.Init();
       
    }

    private void ResetSpellsLevels()
    {
        PlayerPrefs.SetInt("Fire-Level", 0);
        PlayerPrefs.SetInt("Freeze-Level", 0);
        PlayerPrefs.SetInt("Volt-Level", 0);
    }

    public void UseSpell(Spell spell)
    {
        if (spell.IsBlocked) return;

        if (spell.CanUseSpell && spell.mpCost <= _playersStats.mp.CurrentValue)
        {
            SendAnalytics(spell.type);
            spell.UseSpell(spawnPoint.position);
            _playersStats.ConsumeMp(spell.mpCost);
        }
    }

    private void SendAnalytics(SpellType type)
    {
        switch (type)
        {
            case SpellType.Fire:
                GlobalEvent.Instance.Dispatch(new FireBallCasted());
                break;
            case SpellType.Freeze:
                GlobalEvent.Instance.Dispatch(new FreezeBallCasted());
                break;
            case SpellType.Volt:
                GlobalEvent.Instance.Dispatch(new VoltBallCasted());
                break;
            case SpellType.Shield:
                GlobalEvent.Instance.Dispatch(new ShieldBallCasted());
                break;
        }
    }

    void IUpdate.Update()
    {
        if (_playerInputController.Sk1)//Fire{
        {
            UseSpell(_availableSpells[0]);
        }
        if (_playerInputController.Sk2)//Freeze
        {
            UseSpell(_availableSpells[1]);
        }
        if (_playerInputController.Sk3)//Volt.
        {
            UseSpell(_availableSpells[2]);
        }
        if (_playerInputController.Sk4)//Volt.
        {
            UseSpell(_availableSpells[3]);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_availableSpells[0].p, 1f);
    }

    public bool IsBlocked(SpellType spell)
    {
        if ((int)spell >= _availableSpells.Count) return true;
        return _availableSpells[(int)spell].IsBlocked;
    }
}
