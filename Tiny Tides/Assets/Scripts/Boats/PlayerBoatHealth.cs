using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoatHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public int health;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip fireSFX;

    void Start()
    {
        health = MaxHealth;
    }

    void Update()
    {
        if (health <= 0)
        {
            FireSFX();
            Destroy(gameObject);
        }
    }

    void FireSFX()
    {
        audioSource.pitch = 1f + Random.Range(-0.3f, 0.3f);
        audioSource.PlayOneShot(fireSFX);
    }
}

