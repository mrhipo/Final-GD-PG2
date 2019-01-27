﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyStats : MonoBehaviour ,ISpeed{

    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public Animator fsm;
    [HideInInspector]
    public EnemyStates currentState;
    [HideInInspector]
    public LifeObject lifeObject;

    public int experience;

    public float attackRate;
    public float attackRange;
    public float specialAttackRate;

    public float sightDistance;

    public float speed;
    public float damage;

    [HideInInspector]
    public bool canAttack = true;

    private void Awake()
    {
        target = FindObjectOfType<PlayerController>().transform;
        agent = GetComponent<NavMeshAgent>();
        fsm = GetComponent<Animator>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        lifeObject = GetComponent<LifeObject>();
        agent.speed = speed;
        lifeObject.OnDead += OnDead;
        lifeObject.OnLifeChange += OnLifeChanged;
    }

    private void OnLifeChanged()
    {
        animator.SetTrigger("Hit");
    }

    private void OnDead()
    {
        lifeObject.OnLifeChange -= OnLifeChanged;

        fsm.SetTrigger("Dead");
        agent.isStopped = true;
        GetComponent<Collider>().enabled = false;
        GlobalEvent.Instance.Dispatch<ExperiencePickedEvent>(new ExperiencePickedEvent() { amount = experience });
    }

    public Vector3 TargetPosition { get { return target.position; } }
    public Vector3 Position { get { return transform.position; } }
    public float TargetDistance { get { return Vector3.Distance(TargetPosition, Position); } }
    public Vector3 TargetDirection { get { return TargetPosition -  Position; } }

    public float Speed
    {
        get
        {
            return agent.speed;
        }

        set
        {
            agent.speed = value;
            if (value == 0)
                agent.velocity = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, sightDistance);
    }
}
