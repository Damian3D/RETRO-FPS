using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float speed;

    private float bulletLife;
    private float timer;

    Transform player;


    // Start is called before the first frame update
    void Start()
    {
        bulletLife = 15f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        if(timer > bulletLife)
        {
            Destroy(this.gameObject);
        }

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.SendMessage("EnemyHit", damage, SendMessageOptions.DontRequireReceiver);
        }
        Destroy(this.gameObject);
    }
}
