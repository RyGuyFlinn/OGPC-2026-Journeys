using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public bool enemyInRange = false;
    private float enemySeconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemySeconds = 5;
            enemyInRange = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemySeconds--;
        if (enemySeconds < 0)
        {
            enemyInRange = false;
        }
    }
}
