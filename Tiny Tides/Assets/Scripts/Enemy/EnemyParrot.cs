using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyParrot : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] CloseEnemies;
    private GameObject Player;
    private Rigidbody2D rb;
    public float speed = 5f;
    private bool CanAttack = true;
    public bool ReturnToEnemy = false;
    private bool SpawnDelayBool = true;
    private GameObject boss;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        StartCoroutine(AttackTime());
        StartCoroutine(SpawnDelay());
        Player = GameObject.FindGameObjectWithTag("Player");
        boss = GameObject.FindGameObjectWithTag("Boss");
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.IsOnIsland == false || boss == null)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        if (ReturnToEnemy == false)
        {
            Vector3 direction = (Player.transform.position - transform.position).normalized;
            rb.AddForce(direction * speed, ForceMode2D.Force);
            
            if (CanAttack)
            {
                StartCoroutine(Attack());
                CanAttack = false;
            }
        }
        
        if (ReturnToEnemy)
        {
            transform.GetComponent<Rigidbody2D>().drag = 1f;
            speed = 10f;
            
            Vector3 direction = (boss.transform.position - transform.position).normalized;
            rb.AddForce(direction * speed, ForceMode2D.Force);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(1);
        }
        if (collision.CompareTag("Boss") && ReturnToEnemy)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Attack()
    {
        transform.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        transform.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        CanAttack = true;
    }
    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(10f);
        ReturnToEnemy = true;
    }
    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(2f);
        SpawnDelayBool = false;
    }
}
