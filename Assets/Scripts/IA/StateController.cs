using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour, IUpdate
{
    public State currentState;

    public EnemyStats enemyStats;
    public Transform eyes;
    public State reminState;

    public List<Transform> wayPointList;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public int nextWayPoint;

    private Dictionary<CoolDownID, float> coolDowns = new Dictionary<CoolDownID, float>();

    private bool _aiActive;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = enemyStats.moveSpeed;
    }

    private void Start()
    {
        UpdateManager.instance.AddUpdate(this);

        //For Test!
        SetupAI(true);
    }

    public void SetupAI(bool aiActivationFromManager)
    {
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
            currentState = nextState;
    }

    public bool CheckIfCountDownElapsed(CoolDownID id , float duration)
    {
        var result = (coolDowns[id] == 0 || Time.time > coolDowns[id] + duration);
        coolDowns[id] = Time.time;
        return result;
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

public enum CoolDownID
{
    Attack
}
