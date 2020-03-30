using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyAI
{
    EnemyStates enemy;

    private float timer;

    public AttackState(EnemyStates enemy)
    {
        this.enemy = enemy;
    }

    public void UpdateActions()
    {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(enemy.chaseTarget.transform.position, enemy.transform.position);

        if (distance > enemy.attackRange && enemy.onlyMalee == true)
        {
            ToChaseState();
        }
        if (distance > enemy.shootRange && enemy.onlyMalee == false)
        {
            ToChaseState();
        }
        Watch();

        if (distance <= enemy.shootRange && distance > enemy.attackRange && enemy.onlyMalee == false && timer >= enemy.attackDelay)
        {
            Attack(true);
            timer = 0;
        }

        if (distance <= enemy.attackRange && timer >= enemy.attackDelay)
        {
            Attack(false);
            timer = 0;
        }
    }

    void Attack(bool shoot)
    {
        if (shoot == false)
        {
            enemy.chaseTarget.SendMessage("EnemyHit", enemy.maleeDamage, SendMessageOptions.DontRequireReceiver);
        }
        else if (shoot == true)
        {
            GameObject missile = GameObject.Instantiate(enemy.missile, enemy.transform.position, Quaternion.identity);
            missile.GetComponent<Bullet>().speed = enemy.missileSpeed;
            missile.GetComponent<Bullet>().damage = enemy.missileDamage;
        }
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

    public void OnTriggerEnter(Collider enemy)
    {

    }

    public void ToPatrolState()
    {

    }

    public void ToAttackState()
    {

    }

    public void ToAlertState()
    {

    }

    public void ToChaseState()
    {

    }
}
