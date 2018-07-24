using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour {

    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Animator animator;

    public float attackRate;
    public float attackRange;

    public float sightDistance;

    public float speed;
    public float damage;
    private void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        agent = GetComponentInChildren<NavMeshAgent>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        agent.speed = speed;
    }

    public Vector3 TargetPosition { get { return target.position; } }
    public Vector3 Position { get { return transform.position; } }
    public float TargetDistance { get { return Vector3.Distance(TargetPosition, Position); } }

}
