using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasurePickup : MonoBehaviour
{
    public TreasureData treasureData;
    public int value;

    public new AudioClip audio;

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
