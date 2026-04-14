using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Parrot : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject[] CloseEnemies;
    private GameObject ClosestEnemy;
    private Rigidbody2D rb;
    public float speed = 5f;
    private bool CanAttack = true;
    public bool ReturnToPlayer = false;
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        StartCoroutine(AttackTime());
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.IsOnIsland == false)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        if (NearestEnemy() != null && ReturnToPlayer == false)
        {
            Vector3 direction = (NearestEnemy().transform.position - transform.position).normalized;
            rb.AddForce(direction * speed, ForceMode2D.Force);
            
            if (CanAttack)
            {
                StartCoroutine(Attack());
                CanAttack = false;
            }
        }
        if (NearestEnemy() == null)
        {
            ReturnToPlayer = true;
        }
        if (ReturnToPlayer)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * speed, ForceMode2D.Force);
        }
    }
    private GameObject NearestEnemy()
    {
        CloseEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = 10f;
        foreach (GameObject target in CloseEnemies)
        {
            float diff = Vector3.Distance(target.transform.position, transform.position);
            
            if (diff < distance)
            {
                ClosestEnemy = target;
                distance = diff;
                
            }
            
        }
        return ClosestEnemy;
     
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(1, new Vector2(0f, 0f));
        }
        if (collision.CompareTag("Player") && ReturnToPlayer)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator Attack()
    {
        transform.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        transform.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        CanAttack = true;
    }
    IEnumerator AttackTime()
    {
        yield return new WaitForSeconds(10f);
        ReturnToPlayer = true;
    }
}
