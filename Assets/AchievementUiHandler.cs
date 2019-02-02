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

    // Start is called before the first frame update
    void Start()
    {
        GlobalEvent.Instance.AddEventHandler<AchievementCompleteEvent>(OnAchievementComplete);
    }

    private void OnAchievementComplete(AchievementCompleteEvent a)
    {
        switch (a.type)
        {
            case AchievementType.GodMode:
                godMode.SetActive(true);
                break;
            case AchievementType.Finder:
                finder.SetActive(true);
                break;
            case AchievementType.Parrillero:
                parillero.SetActive(true);
                break;
            case AchievementType.ShellShock:
                shellShock.SetActive(true);
                break;
            case AchievementType.Flash:
                theFlash.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
    }
}
