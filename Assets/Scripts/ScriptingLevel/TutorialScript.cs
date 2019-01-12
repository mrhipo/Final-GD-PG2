using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public Light directionalLight;
    public GameObject shotgun;
   // public PlayerController playerInputController;

    public void OnPickUpWeapon()
    {
        shotgun.SetActive(true);
        directionalLight.intensity = .2f;
        //playerInputController.checkTriggerShoot = true;
    }
}
