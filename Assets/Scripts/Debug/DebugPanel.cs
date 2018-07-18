using System;
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

    public void OnToggleLife()
    {
        playerLife.SetActive(!playerLifeToggle.isOn);
    }

    public void OnToggleWizzard()
    {
        if (wizzarToggle.isOn)
            playerStats.OnMpChange += FillMp;
        else
            playerStats.OnMpChange -= FillMp;
    }

    private void FillMp(float p)
    {
        playerStats.mp.CurrentValue += 100000;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
            panel.SetActive(!panel.activeSelf);
    }

}
