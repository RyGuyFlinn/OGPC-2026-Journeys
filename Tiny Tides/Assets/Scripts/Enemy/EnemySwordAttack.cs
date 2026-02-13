using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySwordAttack : MonoBehaviour
{
    public static EnemySwordAttack instance;

    [Header("Attack Settings")]
    public float attackDelay = 0.5f;
    public float attackDuration = 0.2f;
    public int attackDamage = 1;
    public float knockback = 5f;

    [Header("Block Settings")]
    public float blockTime = 1.0f;
    public float blockDelay = 0.5f;
    public float blockReduction = 0.5f;
    public float blockPercentage;

    [Header("References")]
    public Collider2D swordHitbox;
    public Animator animator;
    //public AudioSource audioSource;
    //public AudioClip swingSound;
    //public AudioClip blockSound;

    private bool attacking = false;
    private bool playerAttacking = false;
    private bool playerBlocking = false;
    public bool blocking = false;
    private bool canAttack = true;
    private bool canBlock = true;

    [Header("AttackConditions")]
    public Transform player;
    public float minDistance;
    public float AttackCooldown;

    

    [Header("BlockingConditions")]

    PlayerControls controls;

    [Header("Telegraphing")]
    public GameObject Telegraph;

    [Header("Others")]
    public EnemyMovement enemymovement;

    [Header("Sound Effects")]
    public GameObject Ching;
    void Awake()
    {
        instance = this;

        controls = new PlayerControls();
        
       // controls.GamePlay.Attack.performed += ctx => IfPlayerAttack();
      //  controls.GamePlay.Block.performed += ctx => callBlock();
        
    }

    
    void Update()
    {
        playerAttacking = SwordAttack.instance.attacking;
        playerBlocking = SwordAttack.instance.blocking;

        if (playerAttacking)
        {
            if (Random.Range(0.0f, 1.0f) * 10 <= blockPercentage)
            {
                callBlock();
            }
        }

        float playerdistance = Vector3.Distance(player.position, transform.position);
        if ((playerdistance <= minDistance) && canAttack)
        {  
            callAttack();
        }
        if (playerdistance <= 2.25f)
        {
            enemymovement.agent.speed = 0f;
        }
        else if ((attacking == false) && (playerdistance > 2.25f))
        {
            enemymovement.agent.speed = 2.5f;
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
        Telegraph.SetActive(true);
        enemymovement.agent.speed = 1f;
        yield return new WaitForSeconds(0.5f);
        Telegraph.SetActive(false);
        // Play SFX or animation if you have them
        //if (audioSource && swingSound)
        //    audioSource.PlayOneShot(swingSound);
        
        if (animator) animator.SetTrigger("Attack");
        
        // Enable sword hitbox for a short time
        swordHitbox.enabled = true;
        
        yield return new WaitForSeconds(attackDuration);
        enemymovement.agent.speed = 2.5f;
        swordHitbox.enabled = false;
        attacking = false;
    }

    IEnumerator Block()
    {
        if (!blocking)
        {
            blocking = true;

            if (animator) animator.SetBool("Blocking", true);

            yield return new WaitForSeconds(blockTime); // active blocking phase

            blocking = false;
            if (animator) animator.SetBool("Blocking", false);

            yield return new WaitForSeconds(blockDelay); // cooldown phase
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (attacking)
        {
            if (other.tag == "Player")
            {
                // Check if the object has an enemy health component
                PlayerHealth playerH = other.GetComponent<PlayerHealth>();
                if (playerH != null)
                {
                    // Calculate knockback direction
                    Vector2 direction = (other.transform.position - transform.position).normalized;

                    if (!playerBlocking)
                    {
                        // Apply damage + knockback
                        playerH.TakeDamage(attackDamage);
                    }
                    if (playerBlocking){
                        
                        StartCoroutine(SpecialFunctions.FreezeFrames(Ching));
                    }
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
