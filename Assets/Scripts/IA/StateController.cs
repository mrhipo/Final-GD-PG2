using System;
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
    public ILineOfSight lineOfSight;

    public List<Transform> wayPointList;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public GameObject Target { get { return lineOfSight.Target; } }
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public int nextWayPoint;

    private Dictionary<CoolDownID, float> coolDowns = new Dictionary<CoolDownID, float>();

    private bool _aiActive;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lineOfSight = GetComponentInChildren<ILineOfSight>();
        navMeshAgent.speed = enemyStats.moveSpeed;
    }

    private void Start()
    {
        UpdateManager.instance.AddUpdate(this);

        //For Test!
        SetupAI(true);
        InitCoolDowns();
    }

    private void InitCoolDowns()
    {
        foreach (CoolDownID item in Enum.GetValues(typeof(CoolDownID)))
            coolDowns.Add(item, float.MinValue);
    }

    public void SetupAI(bool aiActivationFromManager)
    {
        navMeshAgent.enabled = _aiActive = aiActivationFromManager;
    }

    void IUpdate.Update()
    {
        if (!_aiActive) return;

        currentState.UpdateState(this);
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
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

    //Checkear distancia del especial attack
    public bool PlayerInAttackRange { get { return Target != null && Vector3.Distance(Target.transform.position, transform.position) < enemyStats.attackRange; } }
    public bool PlayerInSpecialAttackRange { get { return Target != null && Vector3.Distance(Target.transform.position, transform.position) < enemyStats.specialAttackRange.CurrentValue; } }

    public bool CheckIfCountDownElapsed(CoolDownID id , float duration)
    {

        if(Time.time > coolDowns[id] + duration)
        {
            coolDowns[id] = Time.time;
            return true;
        }
        return false;
    }

    public virtual void Attack()
    {
        animator.SetTrigger("Attack");
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

public enum CoolDownID
{
    Attack,
    SpecialAttack
}
