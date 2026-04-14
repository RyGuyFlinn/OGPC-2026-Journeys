using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyClub : MonoBehaviour
{
    public static EnemyClub instance;

    [Header("Attack Settings")]
    public float attackDelay = 0.5f;
    public float attackDuration = 0.2f;
    public int attackDamage = 2;
    public float knockback = 5f;



    [Header("References")]
    public Collider2D swordHitbox;
    public Animator animator;
    //public AudioSource audioSource;
    //public AudioClip swingSound;
    //public AudioClip blockSound;

    private bool attacking = false;
    private bool playerAttacking = false;
    private bool playerBlocking = false;

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
    public GameObject enemy;
    public bool Charge;
    private Vector3 oldplayerpos;
    //[Header("Sound Effects")]
    // public GameObject Ching;
    void Awake()
    {
        instance = this;



        // controls.GamePlay.Attack.performed += ctx => IfPlayerAttack();
        //  controls.GamePlay.Block.performed += ctx => callBlock();

    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = transform.parent.parent.gameObject;
    }


    void Update()
    {
        if (SwordAttack.instance != null) playerAttacking = SwordAttack.instance.attacking;
        if (SwordAttack.instance != null) playerBlocking = SwordAttack.instance.blocking;

       

        float playerdistance = Vector3.Distance(player.position, transform.position);
        if ((playerdistance <= minDistance))
        {
            if (canAttack)
            {
                callAttack();
                StartCoroutine(Dashtime());
            }
            
     
        }
        if (Charge == true)
            {
                Rigidbody2D enemyrb = enemy.GetComponent<Rigidbody2D>();

                Vector2 newPosition = Vector2.MoveTowards(enemyrb.position, oldplayerpos, 5f * Time.fixedDeltaTime);
                enemyrb.MovePosition(newPosition);
                if (Vector3.Distance(oldplayerpos, enemyrb.position) < 1f)
                {
                Charge = false;
                StartCoroutine(FinishAttack());
                }
            }
        
    }
    IEnumerator Dashtime()
    {
        yield return new WaitForSeconds(5f);
        Charge = false;
        if (canAttack == false) StartCoroutine(FinishAttack());
       
    }
    private void callAttack()
    {
        StartCoroutine(StartAttack());
   
    }

    
    IEnumerator StartAttack()
    {
        canAttack = false;
        attacking = true;
        Telegraph.SetActive(true);
        enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        enemymovement.enabled = false;
        
        yield return new WaitForSeconds(0.5f);
        Telegraph.SetActive(false);
        oldplayerpos = player.position;
        Charge = true;
    }
    IEnumerator FinishAttack()
    {
        // Play SFX or animation if you have them
        //if (audioSource && swingSound)
        //    audioSource.PlayOneShot(swingSound);

        if (animator) animator.SetTrigger("Attack");

        // Enable sword hitbox for a short time
        swordHitbox.enabled = true;

        yield return new WaitForSeconds(attackDuration);
        swordHitbox.enabled = false;
        yield return new WaitForSeconds(1f);
        enemy.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        enemymovement.enabled = true;
        enemymovement.agent.speed = 2.5f;
        
        attacking = false;
        yield return new WaitForSeconds(AttackCooldown);
        canAttack = true;
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

                        // Apply damage + knockback
                        playerH.TakeDamage(attackDamage);

                }
            }
        }
    }


}
