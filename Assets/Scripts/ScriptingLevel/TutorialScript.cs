using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialScript : MonoBehaviour
{
    public Light directionalLight;
    public GameObject shotgun;
   // public PlayerInputController playerInputController;

    public GameObject Zombie;
    public GameObject particlePoison;

    GameObject allie;

    public void OnPickUpWeapon()
    {
        allie = FindObjectOfType<Allie>().gameObject;
        shotgun.SetActive(true);
        directionalLight.intensity = .2f;
        //playerInputController.checkTriggerShoot = true;
        particlePoison.SetActive(true);
        DialogueSystem.ShowText("Ohh noo! I don't feel well",3);
        FrameUtil.AfterDelay(5, ActiveZombie);
    }

    public void ActiveZombie()
    {
        allie.transform.parent.gameObject.SetActive(false);
        Zombie.gameObject.SetActive(true);
        Zombie.GetComponent<NavMeshAgent>().Warp(allie.transform.position);
    }
}
