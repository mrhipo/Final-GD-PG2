using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementUiHandler : MonoBehaviour
{
    public GameObject godMode;
    public GameObject parillero;
    public GameObject shellShock;
    public GameObject finder;
    public GameObject theFlash;

    AchievementsEventHandler achievementsEventHandler;
    // Start is called before the first frame update
    void Start()
    {
        if (achievementsEventHandler == null)
            achievementsEventHandler = FindObjectOfType<AchievementsEventHandler>();
        GlobalEvent.Instance.AddEventHandler<AchievementCompleteEvent>(OnAchievementComplete);
    }
    
    private void OnAchievementComplete(AchievementCompleteEvent a)
    {
        UpdateHud();
    }

    public void UpdateHud()
    {
        foreach (var item in achievementsEventHandler.achievements.allAchievements)
        {
            switch (item.type)
            {
                case AchievementType.GodMode:
                    godMode.SetActive(item.completed);
                    break;
                case AchievementType.Finder:
                    finder.SetActive(item.completed);
                    break;
                case AchievementType.Parrillero:
                    parillero.SetActive(item.completed);
                    break;
                case AchievementType.ShellShock:
                    shellShock.SetActive(item.completed);
                    break;
                case AchievementType.Flash:
                    theFlash.SetActive(item.completed);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if(achievementsEventHandler == null)
                achievementsEventHandler = FindObjectOfType<AchievementsEventHandler>();
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
            UpdateHud();
        }
    }
}
