using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    private GameObject player;
    public GameObject bombPrefab;
    public GameObject Telegraph;
    private EnemyMovement enemymovement;
    private bool CanAttack = true;
    private bool RunAway = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemymovement = transform.GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 7f && CanAttack == true && RunAway == false) 
        {
            StartCoroutine(ThrowBomb());
            transform.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0.0f;
            CanAttack = false;
        }
        if (Vector3.Distance(transform.position, player.transform.position) <= 5f)
        {
            RunAway = true;
        }
        if (RunAway == true)
        {
            Vector3 pushDir = transform.position - player.transform.position;


            Rigidbody2D enemyrb = GetComponent<Rigidbody2D>();

            enemyrb.velocity = pushDir.normalized * 2.5f;
        }
        else
        {
            Rigidbody2D enemyrb = GetComponent<Rigidbody2D>();
            enemyrb.velocity = Vector3.zero;
        }
        if (Vector3.Distance(transform.position, player.transform.position) > 7f)
        {
            RunAway = false;
            transform.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 2.5f;

        }
       
    }
    IEnumerator ThrowBomb()
    {
        
        Telegraph.SetActive(true);
        enemymovement.enabled = false;
        
        yield return new WaitForSeconds(0.5f);
        Telegraph.SetActive(false);
        Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;

        GameObject bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
        GameObject bombdamage = bomb.transform.Find("BombCollider").gameObject;
        bombdamage.GetComponent<BombDamage>().IsEnemyBomb = true;
        //ForceMode2D.Impulse
        float dir = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bomb.transform.rotation = Quaternion.AngleAxis(dir - 90f, Vector3.forward);
        bomb.GetComponent<Rigidbody2D>().AddForce(bomb.transform.up * 60f * Vector3.Distance(transform.position, player.transform.position));
        yield return new WaitForSeconds(0.3f);
        enemymovement.enabled = true;
        
        yield return new WaitForSeconds(2f);
        CanAttack = true;

    }
}
