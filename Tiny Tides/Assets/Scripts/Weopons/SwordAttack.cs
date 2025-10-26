using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackDelay = 0.5f;
    public float attackDuration = 0.2f;
    public int attackDamage = 1;
    public float knockback = 5f;

    [Header("Block Settings")]
    public float blockTime = 1.0f;
    public float blockDelay = 0.5f;
    public float blockReduction = 0.5f;

    [Header("References")]
    public Collider2D swordHitbox;
    public Animator animator;
    //public AudioSource audioSource;
    //public AudioClip swingSound;
    //public AudioClip blockSound;

    private bool attacking = false;
    private bool blocking = false;
    private bool canAttack = true;
    private bool canBlock = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack());
        }

        else if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Block());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        attacking = true;

        // Play SFX or animation if you have them
        //if (audioSource && swingSound)
        //    audioSource.PlayOneShot(swingSound);

        if (animator) animator.SetTrigger("Attack");

        // Enable sword hitbox for a short time
        swordHitbox.enabled = true;

        yield return new WaitForSeconds(attackDuration);

        swordHitbox.enabled = false;
        attacking = false;

        // Wait for cooldown before allowing next attack
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    IEnumerator Block()
    {
        if (!blocking)
        {
            blocking = true;
            Debug.Log("Blocking!");

            if (animator) animator.SetBool("Blocking", true);

            yield return new WaitForSeconds(blockTime); // active blocking phase

            blocking = false;
            if (animator) animator.SetBool("Blocking", false);
            Debug.Log("Stopped blocking.");

            yield return new WaitForSeconds(blockDelay); // cooldown phase
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (attacking)
        {
            if (other.tag == "Enemy")
            {
                // Check if the object has an enemy health component
                EnemyHealth enemy = other.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    // Calculate knockback direction
                    Vector2 direction = (other.transform.position - transform.position).normalized;

                    // Apply damage + knockback
                    enemy.TakeDamage(attackDamage, direction * knockback);
                }
            }
        }
    }
}
