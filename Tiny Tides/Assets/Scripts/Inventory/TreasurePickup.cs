using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePickup : MonoBehaviour
{
    public TreasureData treasureData;
    public int value;
    public float speed;
    public float attractionRadius;
    public float playerRadius;
    private GameObject player;

    public new AudioClip audio;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position) - playerRadius;

        if (distance <= attractionRadius)
        {
            if (distance < 1f)
            {
                transform.position = player.transform.position;
            }
            else
            {
                Vector3 newPos = Vector3.Normalize(player.transform.position - transform.position) * (speed / ((distance - 1) * (25 / attractionRadius)) + 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //when collides with player, increase player's treasure score then delete this gameobject
        if (collision.tag == "Player")
        {
            treasureData.ChangeTreasure(value);

            SoundFXManager.Instance.PlaySoundFXClip(audio, transform, 2, 1, true);

            Destroy(gameObject);
        }
    }
}
