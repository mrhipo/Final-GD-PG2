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

    public GameObject firstZombie;
    public GameObject particlePoison;

    public List<GameObject> allZombies;

    GameObject allie;

    public void OnPickUpWeapon()
    {
        allie = FindObjectOfType<Allie>().gameObject;
        shotgun.SetActive(true);
        directionalLight.intensity = .2f;
        //playerInputController.checkTriggerShoot = true;
        particlePoison.SetActive(true);
        DialogueSystem.ShowText("Ohh noo! I don't feel good",3);
        FrameUtil.AfterDelay(3, ()=>
        {
            DialogueSystem.ShowText("Run away from me!", 3);
        });

        FrameUtil.AfterDelay(7, ActiveZombie);
    }

    public void ActiveZombie()
    {
        allie.transform.parent.gameObject.SetActive(false);
        firstZombie.gameObject.SetActive(true);
        firstZombie.GetComponent<NavMeshAgent>().Warp(allie.transform.position);
        //Active AlL Zobmies
        allZombies.ForEach(a => a.gameObject.SetActive(true));
    }
}
