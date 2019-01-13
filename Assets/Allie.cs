using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Allie : MonoBehaviour
{
    public Transform doorWaypoint;
    public Transform InDoorWaypoint;
    public Transform ExploreTransform;

    public GameObject movementTutorial;
    public GameObject LookAroundTutorial;
    public GameObject ShootTutorial;

    public GameObject doorGoForMaterial;
    public Collider doorCollider;

    Action OnPlayerTrigger;

    NavMeshAgent agent;
    Collider triggerCollider;

    //int amountCodeFound;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        triggerCollider = GetComponent<Collider>();
        triggerCollider.enabled = false;
        agent.SetDestination(doorWaypoint.position);
        FrameUtil.AfterFrames(2, () => DialogueSystem.ShowText("Follow Me! We have to find the Epic Weapon!", 6));
        FrameUtil.AfterDelay(7, () => DialogueSystem.ShowText("Help Me Here!!", 3));
        FrameUtil.AfterDelay(2, ShowMovementTutorial);
        FrameUtil.AfterDelay(6, () => triggerCollider.enabled = true);
        doorCollider.enabled = false;
        doorGoForMaterial.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
    }

    private void ShowMovementTutorial()
    {
        movementTutorial.SetActive(true);
        OnPlayerTrigger += ()=>{
            movementTutorial.SetActive(false);
            DialogueSystem.ShowText("I need the security Code!! Take A Look Around!", 2.5f);

            FrameUtil.AfterDelay(3, ()=>DialogueSystem.ShowText("If You Find it, Aim it with the flashLight a while, so I can copy that!", 5));

            ShowLookAroundTutorial();
            OnPlayerTrigger = delegate { };
        };
    }

    private void ShowLookAroundTutorial()
    {
        LookAroundTutorial.SetActive(true);
        FrameUtil.AfterDelay(5, () => LookAroundTutorial.SetActive(false));
    }

    private void OnTriggerEnter(Collider other)
    {
        OnPlayerTrigger();
    }

    public void UnlockDoor()
    {
        doorGoForMaterial.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        doorCollider.enabled = true;
        DialogueSystem.ShowText("It's open! go go go!", 3);
        OnPlayerTrigger = () => {
            agent.SetDestination(ExploreTransform.position);
            OnPlayerTrigger = delegate { };
        };
        agent.SetDestination(InDoorWaypoint.position);

    }


    public void CodeFoundIt()
    {
        /*amountCodeFound++;
        if(amountCodeFound == 1)
        {
            DialogueSystem.ShowText("I need another one, Keep Searching!", 3);
        }
        if (amountCodeFound == 2)
        {
            UnlockDoor();
        }
        */
        UnlockDoor();

    }
}
