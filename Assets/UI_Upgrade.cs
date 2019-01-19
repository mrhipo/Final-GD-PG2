using UnityEngine;

public class UI_Upgrade : MonoBehaviour
{
    public GameObject container;
    public GameObject textK;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            container.SetActive(!container.activeSelf);
            textK.SetActive(!container.activeSelf);
            Mouse.ShowCursor(container.activeSelf);
        }
    }

    private void UpgradeStats(StatType Type)
    {
        GlobalEvent.Instance.Dispatch<StatUpgrade>(new StatUpgrade { type = Type } );
    }

    private void UpgradeSpells(SpellType Type)
    {
        GlobalEvent.Instance.Dispatch<SpellUpgrade>(new SpellUpgrade{ type = Type });
    }

    public void UpgradeHp()
    {
        UpgradeStats(StatType.Hp);
    }
    public void UpgradeMp()
    {
        UpgradeStats(StatType.Mp);
    }
    public void UpgradeMpRecovery()
    {
        UpgradeStats(StatType.MpRecovery);
    }
    public void UpgradeSpeed()
    {
        UpgradeStats(StatType.Speed);
    }

    public void UpgradeFire()
    {
        UpgradeSpells(SpellType.Fire);
    }
    public void UpgradeFreeze()
    {
        UpgradeSpells(SpellType.Freeze);
    }
    public void UpgradeVolt()
    {
        UpgradeSpells(SpellType.Volt);
    }
}
