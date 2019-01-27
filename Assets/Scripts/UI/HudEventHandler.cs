using System;
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

    [Header("Objects")]
    public GameObject credits;

    [Header("Texts")]
    public Text missionObjetive;
   
    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        spellCaster = FindObjectOfType<SpellCaster>();

        playerStats.lifeObject.OnLifeChange += OnUIChange;
        playerStats.OnMpChange += OnUIChange;
      
        GlobalEvent.Instance.AddEventHandler<CreditsPickedEvent>(OnCreditsPicked);
        GlobalEvent.Instance.AddEventHandler<LevelStartEvent>(OnLevelStart);
    }

    private void OnLevelStart(LevelStartEvent objetive)
    {
        missionObjetive.text = objetive.objetive;
    }

    private void OnCreditsPicked(CreditsPickedEvent creditsPicked)
    {
        credits.SetActive(true);
        credits.GetComponentInChildren<Text>().text = "Credits x " + creditsPicked.amount;

        FrameUtil.AfterDelay(3, Close);
    }

    private void Close()
    {
        credits.SetActive(false);
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

