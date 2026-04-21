using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int health;
    public float speed;
    public float attractionRadius;
    public float playerRadius;
    private GameObject player;
    public new AudioClip audio;
    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when collides with player, increase player's treasure score then delete this gameobject
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().currentHealth += health;

            SoundFXManager.Instance.PlaySoundFXClip(audio, transform, 20, 1, false);

            Destroy(gameObject);
        }
    }
    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position) - playerRadius;

        if (distance <= attractionRadius)
        {
            if (distance < 0.5f)
            {
                transform.position = player.transform.position;
            }
            else
            {
                float magnitude = (Time.deltaTime * speed * (attractionRadius - distance)) / (attractionRadius - 1);
                Vector3 newPos = Vector3.Normalize(player.transform.position - transform.position) * magnitude;
                transform.position += newPos;
            }
        }
    }
}
