using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dron : MonoBehaviour, IUpdate
{
    public float speed;
    public Transform[] wayPoints;

    private NavMeshAgent agent;
    private LifeObject _lifeObject;

    void Start()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        agent.speed = speed;

        _lifeObject = GetComponent<LifeObject>();
        _lifeObject.OnDead += () => { };

        FindWayPoint();
        UpdateManager.instance.AddUpdate(this);
    }

    void IUpdate.Update()
    {
        if (agent.remainingDistance <= 0)
        {
            agent.isStopped = true;
            FindWayPoint();
        }
    }

    private void FindWayPoint()
    {
        agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Length)].position);
        agent.isStopped = false;
    }

    private void OnDead()
    {
        _lifeObject.OnDead -= OnDead;
        agent.isStopped = true;
        UpdateManager.instance.RemoveUpdate(this);
    }
}
