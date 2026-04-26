using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerBoatHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public int health;

    public Slider healthSlider;

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
            if (audioSource != null && fireSFX != null) FireSFX();
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player Die");
        SceneManager.LoadScene("YouDied");
    }

    void FireSFX()
    {
        audioSource.pitch = 1f + Random.Range(-0.3f, 0.3f);
        audioSource.PlayOneShot(fireSFX);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyCannonBall"))
        {
            health -= 10;

            healthSlider.value = health;
        }
    }
}

