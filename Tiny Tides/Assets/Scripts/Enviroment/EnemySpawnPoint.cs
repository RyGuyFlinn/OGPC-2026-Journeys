using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public Transform EnemySpawnLocation;
    public GameObject[] Enemies;
    public float Radius;
    [Header("Between 5-10 is probably good")]
    public int Difficulty = 5;
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
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player Collided");
            SpawnEnemies();
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
            if (Enemies[SingleEnemy].GetComponent<EnemyCustomization>().EnemyDifficulty + CurrentDif > Difficulty)
            {
                while (Enemies[SingleEnemy].GetComponent<EnemyCustomization>().EnemyDifficulty + CurrentDif > Difficulty)
                {
                    SingleEnemy++;
                    if (SingleEnemy > Enemies.Length) SingleEnemy = 0;
                }
            }
            
            Instantiate(Enemies[SingleEnemy], new Vector3(EnemySpawnLocation.position.x + Random.Range(-Radius, Radius), EnemySpawnLocation.position.y + Random.Range(-Radius, Radius), EnemySpawnLocation.position.z), transform.rotation);
            CurrentDif += Enemies[SingleEnemy].GetComponent<EnemyCustomization>().EnemyDifficulty;
        }
    }
}
