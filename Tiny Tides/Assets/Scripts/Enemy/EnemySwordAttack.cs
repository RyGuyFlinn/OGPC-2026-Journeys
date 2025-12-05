using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySwordAttack : MonoBehaviour
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
    private bool playerattacking = false;
    private bool blocking = false;
    private bool canAttack = true;
    private bool canBlock = true;

    [Header("AttackConditions")]
    public Transform player;
    public float minDistance;
    public float AttackCooldown;

    [Header("BlockingConditions")]

    PlayerControls controls;

    void Awake()
    {
        
        controls = new PlayerControls();
        
       // controls.GamePlay.Attack.performed += ctx => IfPlayerAttack();
      //  controls.GamePlay.Block.performed += ctx => callBlock();
        
    }
    void Update()
    {
        playerattacking = SwordAttack.instance.attacking;
        if (playerattacking)
        {
            Debug.Log("Player is Attacking");
        }
        float playerdistance = Vector3.Distance(player.position, transform.position);
        if ((playerdistance <= minDistance) && canAttack)
        {
            callAttack();
        }
    }

    private void callAttack()
    {
        StartCoroutine(Attack());
        StartCoroutine(Attackcooldown());
    }
   

    private void callBlock()
    {
        StartCoroutine(Block());
    }
    IEnumerator Attackcooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
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

    void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    void OnDisable()
    {
        controls.GamePlay.Disable();
    }
}
