using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 8;
    public int currentHealth;

    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthText.text = "Health: " + currentHealth.ToString();
        Debug.Log("Health: " + currentHealth.ToString());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player Die");
    }
}
