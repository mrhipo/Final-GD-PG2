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
    }

    private void InitSpell()
    {
        foreach (var item in _availableSpells)
            item.Init();
    }

    public void UseSpell(Spell spell)
    {
        if (!spell.IsUnlocked) return;

        if (spell.CanUseSpell && spell.mpCost <= _playersStats.mp.CurrentValue)
        {
            spell.UseSpell(spawnPoint.position);
            _playersStats.ConsumeMp(spell.mpCost);
        }
    }

    void IUpdate.Update()
    {
        if (_playerInputController.Sk1)//Fire
            UseSpell(_availableSpells[0]);
        if (_playerInputController.Sk2)//Freeze
            UseSpell(_availableSpells[1]);
        if (_playerInputController.Sk3)//Volt.
            UseSpell(_availableSpells[2]);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(_availableSpells[0].p, 1f);
    }
}
