using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dron : MonoBehaviour, IUpdate
{
    public float speed;
    public Wp[] wayPoints;

    private NavMeshAgent agent;
    private LifeObject _lifeObject;
    public GameObject explosion;

    void Start()
    {
        wayPoints = FindObjectsOfType<Wp>();
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
        agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Length)].transform.position);
        agent.isStopped = false;
    }

    private void OnDead()
    {
        _lifeObject.OnDead -= OnDead;
        agent.isStopped = true;
        UpdateManager.instance.RemoveUpdate(this);
        SoundManager.instance.PlayFX("Explosion");
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
