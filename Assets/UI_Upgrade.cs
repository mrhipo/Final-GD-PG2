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
        }
    }

    private void UpgradeStats(StatType Type)
    {
        GlobalEvent.Instance.Dispatch<StatUpgrade>(new StatUpgrade { type = Type } );
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
}
