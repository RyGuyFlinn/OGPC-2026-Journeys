using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoatHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public int health;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip DeathSFX;

    void Start()
    {
        health = MaxHealth;
    }

    void Update()
    {
        if (health <= 0)
        {
            SoundFXManager.Instance.PlaySoundFXClip(DeathSFX, transform, 1f, 1f);
            Destroy(gameObject);
        }

    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CannonBall"))
        {
            health -= 10;
        }
    }
}
