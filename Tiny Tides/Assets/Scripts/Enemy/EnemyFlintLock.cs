using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyFlintLock : MonoBehaviour
{
    public static EnemyFlintLock instance;

    [Header("Attack Settings")]
    public float attackDelay = 2f;
    public int attackDamage = 1;

    [Header("References")]
    public Animator animator;

    [Space]
    public GameObject bulletPrefab;
    public GameObject muzzle;

    public AudioSource audioSource;
    public AudioClip shootSound;

    public bool attacking = false;
    public bool blocking = false;
    public bool canAttack = true;

    public GameObject player;
    public float minDistance = 30f;
    public EnemyMovement enemymovement;
    public GameObject Telegraph;
    private GameObject enemy;
    private int Randomdirx;
    private int Randomdiry;
    private float randomswitch = 1f;
    private float switchtime = 2f;
    [Header("Macaw Boss")]
    public bool FlintLockActive = true;
    public bool IsCaptainMacaw = false;
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        enemymovement = GetComponentInParent<EnemyMovement>();
        enemy = transform.parent.parent.gameObject;
    }
    void Update()
    {
        if (FlintLockActive == true){
        float playerdistance = Vector3.Distance(player.transform.position, transform.position);

        if (playerdistance <= minDistance)
        {
            if (canAttack) CallAttack();
            enemy.GetComponent<NavMeshAgent>().enabled = false;
            enemymovement.enabled = false;
            Rigidbody2D enemyrb = enemy.GetComponent<Rigidbody2D>();
            if (attacking == false)
            {
                switchtime += Time.deltaTime;
                if (switchtime >= randomswitch)
                {
                    switchtime = 0;
                    randomswitch = Random.Range(0.5f, 1.5f);
                    Randomdirx = Random.Range(-1, 2);
                    Randomdiry = Random.Range(-1, 2);
                    if (Randomdirx == 0 && Randomdiry == 0)
                    {
                        if (Random.Range(0, 2) == 0) Randomdirx -= 1;
                        else Randomdirx += 1;
                        if (Random.Range(0, 2) == 0) Randomdiry -= 1;
                        else Randomdiry += 1;
                    }
                }
                enemyrb.velocity = new Vector2(2.5f * Randomdirx, 2.5f * Randomdiry);
            }
        }
        if (playerdistance > minDistance)
        {
            enemy.GetComponent<NavMeshAgent>().enabled = true;
            enemymovement.enabled = true;
        }
        }
    }
    private void CallAttack()
    {
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        Rigidbody2D enemyrb = enemy.GetComponent<Rigidbody2D>();
        enemyrb.velocity = new Vector2(0f, 0f);
        canAttack = false;
        attacking = true;
        Telegraph.SetActive(true);
        enemymovement.agent.speed = 0f;
        yield return new WaitForSeconds(0.3f);
        Telegraph.SetActive(false);
        enemymovement.agent.speed = 2.5f;
        // Play SFX or animation if you have them
        /*
        if (audioSource && shootSound)
            audioSource.pitch = 1f + Random.Range(-0.3f, 0.3f);
        audioSource.PlayOneShot(shootSound);
        audioSource.pitch = 1f;

        if (animator) animator.SetTrigger("Shoot");
        */
        Debug.Log("Shoot");
        float Randomaim = Random.Range(-20f, 21f);
        if (IsCaptainMacaw) {
            Randomaim = Random.Range(-40f, 41f);
            StartCoroutine(SecondAttack());
        }
        Quaternion offset = Quaternion.Euler(0, 0, Randomaim);
        GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation * offset);
        if (IsCaptainMacaw) bullet.GetComponent<Bullet>().speed = 10;
        /*
         * 
        GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.Euler(muzzle.transform.rotation.x, muzzle.transform.rotation.y, muzzle.transform.rotation.z + Randomaim));
        Debug.Log(Randomaim);
         */
        attacking = false;

        // Wait for cooldown before allowing next attack
        
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
    IEnumerator SecondAttack(){
        yield return new WaitForSeconds(0.1f);
        float Randomaim = Random.Range(-40f, 41f);
        Quaternion offset = Quaternion.Euler(0, 0, Randomaim);
        GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation * offset);
        bullet.GetComponent<Bullet>().speed = 10;
    }
}
