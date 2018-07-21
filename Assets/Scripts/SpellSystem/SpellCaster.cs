using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour, IUpdate
{
    public List<Spell> _availableSpells;

    private PlayerStats _playersStats;
    private PlayerInputSpell _playerInputController;

    private void Start()
    {
        _playerInputController = new PlayerInputSpell();
        _playersStats = FindObjectOfType<PlayerStats>();
        UpdateManager.instance.AddUpdate(this);
    }

    public void UseSpell(Spell spell)
    {
        if (!spell.IsUnlocked) return;

        if (spell.CanUseSpell && spell.mpCost <= _playersStats.mp.CurrentValue)
        {
            spell.UseSpell(transform.position, transform.forward);
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
}
