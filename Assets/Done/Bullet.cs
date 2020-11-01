using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private Rigidbody2D rigidbody2D;

    public void Cast(Vector2 vector, float speed)
    {
        rigidbody2D.velocity = vector.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagableByEnemies damagable = collision.gameObject.GetComponent<IDamagableByEnemies>();
        //If colliding object implements interface for being damaged by enemies, damage it.
        if (damagable != null)
        {
            damagable.GetDamage(damage);
        }

        //Disable bullet after hitting something
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        //Cache
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
}