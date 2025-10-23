using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorController : MonoBehaviour
{
    public Slider shirtsSlider;
    public Slider pantsSlider;

    public SpriteRenderer shirt;
    public SpriteRenderer pants;

    [Range(0f, 1f)] public float saturation = 1f;
    [Range(0f, 1f)] public float brightness = 1f;

    float shirtHue;
    float pantsHue;

    void Start()
    {
        shirtsSlider.maxValue = 1;
        pantsSlider.maxValue = 1;

        shirtsSlider.minValue = 0;
        pantsSlider.minValue = 0;
    }

    void Update()
    {
        shirtHue = shirtsSlider.value;
        pantsHue = pantsSlider.value;

        shirt.color = Color.HSVToRGB(shirtHue, saturation, brightness);
        pants.color = Color.HSVToRGB(pantsHue, saturation, brightness);
    }
}
