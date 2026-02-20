using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    public int attackDamage = 1;
    public float speed = 5;
    public float knockback = 5f;

    public Rigidbody2D bullet;

    // Update is called once per frame
    void Start()
    {
        bullet.velocity = transform.right * speed;

        StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5);
        
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
            Vector2 direction = (other.transform.position - transform.position).normalized;

            enemy.TakeDamage(attackDamage, direction * knockback);
        }

        Destroy(gameObject);
    }
}
