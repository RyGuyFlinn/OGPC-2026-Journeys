using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCustomization : MonoBehaviour
{
    public SpriteRenderer shirt;
    public SpriteRenderer pants;
    
    [Range(0f, 1f)] public float saturation = 0.7f;
    [Range(0f, 1f)] public float brightness = 0.7f;
    [Header("How Hard The Enemy Is")]
    public int EnemyDifficulty = 1;
    float shirtHue;
    float pantsHue;

    void Start()
    {
        shirtHue = Random.Range(0f, 1f);
        pantsHue = Random.Range(0f, 1f);

        shirt.color = Color.HSVToRGB(shirtHue, saturation, brightness);
        pants.color = Color.HSVToRGB(pantsHue, saturation, brightness);
    }
}
