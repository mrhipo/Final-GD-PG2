using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Upgrade : MonoBehaviour
{
    public GameObject container;
    public GameObject textK;


    int currentHpLevel;
    int currentMpLevel;        
    int currentMpRecoveryLevel;
    int currentSpeedLevel;     
    
    int currentFireLevel;
    int currentFreezeLevel;   
    int currentVoltLevel;
    PlayerStats stat;
    SpellCaster spells;

    public Text hpCost;
    public Text mpCost;
    public Text mpRecoveryCost;
    public Text speedCost;

    public Text fireCost;
    public Text FreezeCost;
    public Text VoltCost;

    public Text creditsTxt;

    public Text experienceTxt;

    private void Start()
    {
        spells = FindObjectOfType<SpellCaster>();
        stat = FindObjectOfType<PlayerStats>();
    }

    private void UpdateDisplay()
    {
        DisableBlockedSpells();
        
        currentHpLevel = stat.currentHpLevel;
        currentMpLevel = stat.currentMpLevel;
        currentMpRecoveryLevel = stat.currentMpRecoveryLevel;
        currentSpeedLevel = stat.currentSpeedLevel;

        currentFireLevel = stat.fireLevel;
        currentFreezeLevel = stat.freezeLevel;
        currentVoltLevel = stat.voltLevel;

        creditsTxt.text = "Credits: " + stat.credits;
        experienceTxt.text = "Experience: " + stat.experience;


        hpCost.text = "Hp (-" + stat.GetCostUpgrade(currentHpLevel) + ")";
        mpCost.text = "Mp (-" + stat.GetCostUpgrade(currentMpLevel) + ")";
        mpRecoveryCost.text = "Mp Recovery (-" + stat.GetCostUpgrade(currentMpRecoveryLevel) + ")";
        speedCost.text = "Speed (-" + stat.GetCostUpgrade(currentSpeedLevel) + ")";

        fireCost.text = "Fire (-" + stat.GetCostUpgrade(currentFireLevel) + ")";
        FreezeCost.text = "Freeze (-" + stat.GetCostUpgrade(currentFreezeLevel) + ")";
        VoltCost.text = "Volt (-" + stat.GetCostUpgrade(currentVoltLevel) + ")";
    }

    private void DisableBlockedSpells()
    {
        fireCost.transform.parent.gameObject.SetActive(!spells.IsBlocked(SpellType.Fire));
        FreezeCost.transform.parent.gameObject.SetActive(!spells.IsBlocked(SpellType.Freeze));
        VoltCost.transform.parent.gameObject.SetActive(!spells.IsBlocked(SpellType.Volt));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            container.SetActive(!container.activeSelf);
            textK.SetActive(!container.activeSelf);
            Mouse.ShowCursor(container.activeSelf);
            if (container.activeSelf)
            {
                UpdateDisplay();
            }
        }
    }

    private void UpgradeStats(StatType Type)
    {
        GlobalEvent.Instance.Dispatch<StatUpgrade>(new StatUpgrade { type = Type } );
        UpdateDisplay();
    }

    private void UpgradeSpells(SpellType Type)
    {
        GlobalEvent.Instance.Dispatch<SpellUpgrade>(new SpellUpgrade{ type = Type });
        UpdateDisplay();
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
