using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 8;
    public int currentHealth;
    public GameObject[] Health;
    public TextMeshProUGUI healthText;
    public Sprite FullHealth;
    public Sprite HalfHealth;
    public Sprite NoHealth;
    public bool HasExtraHealth = false;
    public GameObject ExtraHealth;
    private bool lockhealthgain = false;
    public bool hasShieldEquipped = false;
    public AudioClip hurtsound;
    private bool CanTakeDamage = true;
    public bool BejewledSkullAbility = false;
    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (HasExtraHealth && !lockhealthgain)
        {
            maxHealth = 10;
            ExtraHealth.SetActive(true);
            currentHealth += 2;
            lockhealthgain = true;
        }
        else if (!HasExtraHealth && lockhealthgain) 
        {
            maxHealth = 8;
            ExtraHealth.SetActive(false);
            currentHealth -= 2;
            lockhealthgain = false;
        }
        UpdateHealthDisplay();
    }

    public void TakeDamage(int damage)
    {
        if (CanTakeDamage)
        {
            currentHealth -= damage;
            SoundFXManager.Instance.PlaySoundFXClip(hurtsound, transform, 1f, 1f);
            Debug.Log("Health: " + currentHealth.ToString());
            StartCoroutine(InvincibilityFrames());
        }

        if (currentHealth <= 0 && BejewledSkullAbility == false)
        {
            Die();
        }
        else if (currentHealth <= 0 && BejewledSkullAbility == true)
        {
            currentHealth += 4;
            InventoryManager.LoseBejeweledSkull = true;
        }
    }
    IEnumerator InvincibilityFrames()
    {
        CanTakeDamage = false;
        yield return new WaitForSeconds(1f);
        CanTakeDamage = true;
    }
    public void Die()
    {
        Debug.Log("Player Die");
        SceneManager.LoadScene("YouDied");
    }
    public void UpdateHealthDisplay()
    {
        int count = 2;
        if (currentHealth == 10) ExtraHealth.GetComponent<Image>().sprite = FullHealth;
        if (currentHealth == 9) ExtraHealth.GetComponent<Image>().sprite = HalfHealth;
        if (currentHealth == 8) ExtraHealth.GetComponent<Image>().sprite = NoHealth;
        
        //clamp current health at the max
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        
            foreach (GameObject healthicon in Health)
            {
                if (currentHealth >= count)
                {
                    healthicon.GetComponent<Image>().sprite = FullHealth;
                }
                else
                {
                    if (currentHealth == count - 1)
                    {
                        healthicon.GetComponent<Image>().sprite = HalfHealth;
                    }
                    else
                    {
                        healthicon.GetComponent<Image>().sprite = NoHealth;
                    }
                    
                }
                count += 2;
            }
        
    }
}
