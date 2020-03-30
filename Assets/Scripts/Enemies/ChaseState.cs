using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyAI
{
    EnemyStates enemy;

    public ChaseState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        Watch();
        Chase();
    }

    void Watch()
    {
        RaycastHit hit;

        if (Physics.Raycast(enemy.transform.position, enemy.vision.forward, out hit, enemy.patrolRange) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            enemy.lastKnownPosition = hit.transform.position;
        }

        else
        {
            ToAlertState();
        }
    }

    void Chase()
    {
        enemy.navMeshAgent.destination = enemy.chaseTarget.position;
        enemy.navMeshAgent.isStopped = false;

        if(enemy.navMeshAgent.remainingDistance <= enemy.attackRange && enemy.onlyMalee == true)
        {
            enemy.navMeshAgent.isStopped = true;
            ToAttackState();
        }
        else if(enemy.navMeshAgent.remainingDistance <= enemy.shootRange && enemy.onlyMalee == false)
        {
            enemy.navMeshAgent.isStopped = true;
            ToAlertState();
        }
    }

    public void OnTriggerEnter(Collider enemy)
    {

    }

    public void ToPatrolState()
    {
        Debug.Log("I CAN'T DO IT");
    }

    public void ToAttackState()
    {
        Debug.Log("I AM ATTACKING PLAYER");
        enemy.currentState = enemy.attackState;
    }

    public void ToAlertState()
    {
        Debug.Log("I LOST PLAYER");
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {
        Debug.Log("I AM CHASING");
    }
}
