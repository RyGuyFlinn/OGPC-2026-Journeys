using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordFlipping : MonoBehaviour
{
    private Transform player;
    private bool facingRight = false;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (!player) return;

        if (player.position.x > transform.position.x && facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && !facingRight)
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
