using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public int health;

    public bool canMaleeAttack;
    public bool canShoot;

    public float maleeDamage;
    public float shootDamage;

    public void pistolHit(int damage)
    {
        health = health - damage;
    }
}
