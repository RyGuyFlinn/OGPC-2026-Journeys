using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int health;

    public new AudioClip audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when collides with player, increase player's treasure score then delete this gameobject
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().currentHealth += health;

            SoundFXManager.Instance.PlaySoundFXClip(audio, transform, 5, 1, false);

            Destroy(gameObject);
        }
    }
}
