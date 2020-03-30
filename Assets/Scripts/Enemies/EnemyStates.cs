using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStates : MonoBehaviour
{
    public Transform[] waypoints;

    public int patrolRange;
    public int attackRange;
    public int shootRange;

    public Transform vision;

    public float stayAlertTime;
    public float missileDamage;
    public float missileSpeed;
    public float maleeDamage;
    public float attackDelay;

    public bool onlyMalee = false;

    public GameObject missile;

    [HideInInspector]
    public AlertState alertState;
    [HideInInspector]
    public AttackState attackState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public PatrolState patrolState;
    [HideInInspector]
    public IEnemyAI currentState;
    [HideInInspector]
    public NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Transform chaseTarget;
    [HideInInspector]
    public Vector3 lastKnownPosition;

    private void Awake()
    {
        alertState = new AlertState(this);
        attackState = new AttackState(this);
        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = patrolState;
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateActions();
    }

    private void OnTriggerEnter(Collider otherObj)
    {
        currentState.OnTriggerEnter(otherObj);
    }

    void HiddenShot(Vector3 shotPosition)
    {
        Debug.Log("SOMEONE SHOT");
        lastKnownPosition = shotPosition;
        currentState = alertState;
    }
}
