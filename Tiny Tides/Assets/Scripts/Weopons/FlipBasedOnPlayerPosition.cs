using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipBasedOnPlayerPosition : MonoBehaviour
{
    private Transform player;
    private bool facingRight = true;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (!player) return;

        if (player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
        else if (player.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
