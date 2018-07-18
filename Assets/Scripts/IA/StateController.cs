using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour, IUpdate
{
    public State currentState;

    public EnemyStats enemyStats;
    public Transform eyes;
    public State reminState;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    //Componente para dañar enemigo.
    /*[HideInInspector]*/
    public List<Transform> wayPointList;

    public int nextWayPoint;
    public Transform chaseTarget;
    public float stateTimeElapsed;

    private bool _aiActive;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        UpdateManager.instance.AddUpdate(this);

        //For Test!
        SetupAI(true);
    }

    public void SetupAI(bool aiActivationFromManager/*, List<Transform> wayPointFromManager*/)
    {
        //wayPointList = wayPointFromManager;
        navMeshAgent.enabled = _aiActive = aiActivationFromManager;
    }

    void IUpdate.Update()
    {
        if (!_aiActive) return;

        currentState.UpdateState(this);
    }

    private void OnDestroy()
    {
        UpdateManager.instance.RemoveUpdate(this);
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != reminState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;

        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }

    private void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
        }
    }
}
