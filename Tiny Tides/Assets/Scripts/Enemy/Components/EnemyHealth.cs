using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 5;
    private int currentHealth;

    private Rigidbody2D rb;

    private bool isKnocked = false;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, Vector2 knockback)
    {
        if (isKnocked) return; // prevent stacking knockbacks

        currentHealth -= damage;
        Debug.Log($"{gameObject.name} took {damage} damage! Remaining: {currentHealth}");

        // Apply knockback if there's a Rigidbody2D
        if (rb != null)
        {
            StartCoroutine(DoKnockback(knockback));
        }

        // Death check
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator DoKnockback(Vector2 force)
    {
        isKnocked = true;
        rb.velocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.2f); // short knockback duration
        isKnocked = false;
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} died!");
        Destroy(gameObject);
    }
}
