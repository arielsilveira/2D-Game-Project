using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    [Header("Projectile properties")]
    [SerializeField] public Rigidbody2D rigidbody;
    [SerializeField] public float speed;
    [SerializeField] public float projectileCount = 5;

    public void Update()
    {
        projectileCount -= Time.deltaTime;
        if(projectileCount <= 0) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rigidbody.linearVelocity = new Vector2(-speed, rigidbody.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
        }

        if(collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
