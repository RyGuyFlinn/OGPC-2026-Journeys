using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColorController : MonoBehaviour
{
    public SpriteRenderer shirt;
    public SpriteRenderer pants;

    [Range(0f, 1f)] public float saturation = 0.7f;
    [Range(0f, 1f)] public float brightness = 0.7f;

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
