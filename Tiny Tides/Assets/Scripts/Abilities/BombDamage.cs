using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamage : MonoBehaviour
{
    public bool IsEnemyBomb = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("Boss")) && IsEnemyBomb == false)
        {
            other.GetComponent<EnemyHealth>().TakeDamage(2, new Vector2(0f, 0f));
        }
        if (other.CompareTag("Player") && IsEnemyBomb == true)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(2);
        }
    }
}
