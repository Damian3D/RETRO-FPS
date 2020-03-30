using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyAI
{
    EnemyStates enemy;

    private float timer = 0f;

    public AlertState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        Search();
        Watch();
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
            LookAround();
    }

    void Search()
    {
        enemy.navMeshAgent.destination = enemy.lastKnownPosition;
        enemy.navMeshAgent.isStopped = false;
    }

    void Watch()
    {
        RaycastHit hit;

        if (Physics.Raycast(enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            enemy.navMeshAgent.destination = hit.transform.position;
            ToChaseState();
        }
    }

    void LookAround()
    {
        timer += Time.deltaTime;

        if (timer >= enemy.stayAlertTime)
        {
            timer = 0;
            ToPatrolState();
        }
    }

    public void OnTriggerEnter(Collider enemy)
    {

    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void ToAttackState()
    {
        Debug.Log("I CAN'T DO IT");
    }

    public void ToAlertState()
    {
        Debug.Log("I AM ALERTED");
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }
}
