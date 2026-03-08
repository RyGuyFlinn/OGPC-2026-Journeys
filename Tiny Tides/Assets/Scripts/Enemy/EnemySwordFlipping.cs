using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordFlipping : MonoBehaviour
{
    private Transform player;
    private bool facingRight = false;
    private Transform enemy;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        enemy = transform.parent.parent;
    
    }

    void Update()
    {
        if (!player) return;

        if (enemy.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (enemy.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        //Flips the parents y to properly handle animation flipping
        facingRight = !facingRight;
        Vector3 scale = transform.parent.localScale;
        //scale.x *= -1;
        scale.y *= -1;
        transform.parent.localScale = scale;
    }
}
