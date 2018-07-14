using System.Collections.Generic;
using UnityEngine;

public class SpellCaster : MonoBehaviour, IUpdate
{
    public List<Spell> _availableSpells;

    private PlayerStats _playersStats;

    private void Start()
    {
        _playersStats = FindObjectOfType<PlayerStats>();
        UpdateManager.instance.AddUpdate(this);
    }

    public void UseSpell(Spell spell)
    {
        if (spell.CanUseSpell && spell.mpCost <= _playersStats.mp.CurrentValue)
        {
            spell.UseSpell(transform.position, transform.forward);
            _playersStats.ConsumeMp(spell.mpCost);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            UseSpell(_availableSpells[0]);
    }
}
