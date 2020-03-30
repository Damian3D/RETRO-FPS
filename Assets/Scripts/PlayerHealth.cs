using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;

    AudioSource source;
    public AudioClip hit;

    private float health;

    public FlashScreen flash;

    void Start()
    {
        health = maxHealth;
        source = GetComponent<AudioSource>();
    }

    void EnemyHit (float damage)
    {
        source.PlayOneShot(hit);
        health -= damage;
        flash.TookDamage();
    }
}
