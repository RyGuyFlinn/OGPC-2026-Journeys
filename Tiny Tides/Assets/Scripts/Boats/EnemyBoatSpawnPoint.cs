using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoatSpawnPoint : MonoBehaviour
{
    public Transform EnemySpawnLocation;
    public GameObject[] Enemies;
    public float Radius;
    [Header("Between 1-3 is probably good")]
    public int Difficulty = 2;
    private bool active = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBoat") && active)
        {
            Debug.Log("Player Collided");
            SpawnEnemies();
            active = false;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(EnemySpawnLocation.position, Radius);
    }
    void SpawnEnemies()
    {
        int SingleEnemy;
        int CurrentDif = 0;
        bool Active = true;
        while (Active)
        {
            SingleEnemy = Random.Range(0, Enemies.Length);
            if (CurrentDif == Difficulty)
            {
                Active = false;
                return;
            }
            CurrentDif++;

            Instantiate(Enemies[SingleEnemy], new Vector3(EnemySpawnLocation.position.x + Random.Range(-Radius, Radius), EnemySpawnLocation.position.y + Random.Range(-Radius, Radius), EnemySpawnLocation.position.z), transform.rotation);
       
        }
    }
}
