using UnityEngine;
using UnityEngine.UI;

public class HudEventHandler : MonoBehaviour, IUpdate
{
    PlayerStats playerStats;
    SpellCaster spellCaster;

    [Header("Images")]
    public Image healthFill;
    public Image manaFill;
    public Image fireballCD;
    public Image freezeCD;
    public Image voltCD;
    public Image shieldCD;


    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        spellCaster = FindObjectOfType<SpellCaster>();

        playerStats.lifeObject.OnLifeChange += OnUIChange;
        playerStats.OnMpChange += OnUIChange;
    }


    public void Update()
    {
        //fireballCD.fillAmount = spellCaster.
        //freezeCD.fillAmount = spellCaster.
        //voltCD.fillAmount = spellCaster.
        //shieldCD.fillAmount = spellCaster.
    }

    public void OnUIChange()
    {
        healthFill.fillAmount = playerStats.lifeObject.hp.Percentage;
        manaFill.fillAmount = playerStats.mp.Percentage;

    }
}

