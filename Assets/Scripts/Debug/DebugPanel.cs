﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour {

    public GameObject panel;

    [Header("Model")]
    public LifeObject playerLife;
    public PlayerStats playerStats;

    [Header("UI")]
    public Toggle playerLifeToggle;
    public Toggle wizzarToggle;
    
    [Header("Skills")]
    public Toggle Sk1;
    public Toggle Sk2;
    public Toggle Sk3;
    public Toggle Sk4;

    private void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        playerLife = playerStats.lifeObject;

        playerLifeToggle.onValueChanged.AddListener(OnToggleLife);
        wizzarToggle.onValueChanged.AddListener(OnToggleWizzard);

        Sk1.onValueChanged.AddListener((b)=>OnChangeToggleSkill(b,0));
        Sk2.onValueChanged.AddListener((b)=>OnChangeToggleSkill(b,1));
        Sk3.onValueChanged.AddListener((b)=>OnChangeToggleSkill(b,2));
        Sk4.onValueChanged.AddListener((b)=>OnChangeToggleSkill(b,3));
    }


    private void OnChangeToggleSkill(bool boolean, int i)
    {
        if(boolean)
            GlobalEvent.Instance.Dispatch(new NewSkillEvent((SpellType)i, Color.red, null));
    }

    private void OnToggleLife(bool isOn)
    {
        playerLife.SetActive(!isOn);
    }

    private void OnToggleWizzard(bool isOn)
    {
        if (isOn)
            playerStats.OnMpChange += FillMp;
        else
            playerStats.OnMpChange -= FillMp;
    }

    private void FillMp()
    {
        playerStats.mp.CurrentValue += 100000;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            panel.SetActive(!panel.activeSelf);
            Mouse.ShowCursor(panel.activeSelf);
        }
    }

}
