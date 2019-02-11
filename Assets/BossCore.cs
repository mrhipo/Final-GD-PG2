using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCore : MonoBehaviour
{
    LifeObject life;
    public BossShield bossShield;

    public Color color75;
    public Color color50;
    public Color color25;

    Animator animator;

    public Image hpFillImage;

    bool life_75;
    bool life_50;
    bool life_25;

    private ParticleSystem ps;

    private void Start()
    {
        animator = GetComponent<Animator>();
        life = GetComponentInChildren<LifeObject>();
        ps = GetComponent<ParticleSystem>();
        life.OnLifeChange += OnLifeChanged;
        life.OnDead += OnDead;

    }

    private void OnDead()
    {
        life.OnDead -= OnDead;
        Debug.Log("End Of Game");
    }

    private void OnLifeChanged()
    {
        hpFillImage.fillAmount = Percentage;
        animator.SetTrigger("Hit");
        if (Percentage < .25f && !life_25)
        {
            bossShield.speedRotation = 20;
            bossShield.c.enabled = true;
            life_25 = true;
            var main = ps.main;
            main.startColor = color25;
        }
        else if(Percentage < .5f && !life_50)
        {
            bossShield.speedRotation = 20;
            bossShield.c.enabled = true;
            life_50 = true;
            var main = ps.main;
            main.startColor = color50;

        }
        else if (Percentage < .75f && !life_75)
        {
            bossShield.speedRotation = 20;
            bossShield.c.enabled = true;
            life_75 = true;
            var main = ps.main;
            main.startColor = color75;
        }
    }

    public float Percentage
    {
        get { return life.hp.Percentage; }
    }
}
