using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordAttack : MonoBehaviour
{
    public static SwordAttack instance;
    public PlayerMovement playermovement;
    [Header("Attack Settings")]
    public float attackDelay = 0.1f;
    public float attackDuration = 0.1f;
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

    public bool attacking = false;
    public bool blocking = false;
    private bool enemyBlocking;
    private bool canAttack = true;
    private bool canBlock = true;

    //Combo Variables
    private float PlayerCPS;
    private int Combo;
    PlayerControls controls;

    //Just for testing
    public GameObject Ching;
    void Awake()
    {
        instance = this;
        controls = new PlayerControls();

        controls.GamePlay.Attack.performed += ctx => callAttack();
        controls.GamePlay.Block.performed += ctx => callBlock();

        playermovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        PlayerCPS += Time.deltaTime;
        if (Combo >= 3){
            playermovement.speed = 1f;
            attackDelay = 0f;
        }
        if (PlayerCPS > 0.35f){
            playermovement.speed = 5f;
            attackDelay = 0.1f;
        }
        try {
            enemyBlocking = EnemySwordAttack.instance.blocking;
        }
        catch {
            
        }
    }

    private void callAttack()
    {
        
        if (PlayerCPS <= 0.35f){
            Combo += 1;
        }
        else {
            Combo = 0;
        }
        PlayerCPS = 0;
        if (canAttack) StartCoroutine(Attack());
    }

    private void callBlock()
    {
        StartCoroutine(Block());
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
        Debug.Log("Damage");
        yield return new WaitForSeconds(attackDuration);

        swordHitbox.enabled = false;
        attacking = false;
     
        // Wait for cooldown before allowing next attack
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
     //   Debug.Log("Swing, swung, I don't have a pun");
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
            if (other.tag == "Enemy")
            {
                // Check if the object has an enemy health component
                EnemyHealth enemy = other.GetComponent<EnemyHealth>();
                EnemySwordAttack enemysword = other.GetComponentInChildren<EnemySwordAttack>();
                if (enemy != null)
                {
                    // Calculate knockback direction
                    Vector2 direction = (other.transform.position - transform.position).normalized;
                    Debug.Log("Player Attack");

                    if (!enemyBlocking)
                    {
                        // Apply damage + knockback
                        enemy.TakeDamage(attackDamage, direction * knockback);
                    }
                    else {
                        Debug.Log("Blocked!");
                        StartCoroutine(SpecialFunctions.FreezeFrames(Ching));
                        enemysword.blocking = false;
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
