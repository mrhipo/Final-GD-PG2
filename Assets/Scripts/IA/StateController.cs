using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour, IUpdate 
{
    public State currentState;

    public EnemyStats enemyStats;
    public Transform eyes;
    public State reminState;
    public Animator animator;
    

    public List<Transform> wayPointList;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public int nextWayPoint;

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
        {
            currentState = nextState;
            OnExitCurrentState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;

        return (stateTimeElapsed >= duration);
    }

    private void OnExitCurrentState()
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

    public virtual void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public virtual void Move()
    {
        animator.SetTrigger("Move");
    }

    public virtual void Idle()
    {
        animator.SetTrigger("Idle");
    }

    public virtual void SpecialAttack()
    {
        animator.SetTrigger("SpecialAttack");
    }

    public virtual void Dead()
    {
        animator.SetTrigger("Dead");
    }
}
