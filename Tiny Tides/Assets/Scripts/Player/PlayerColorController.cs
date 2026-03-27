using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorController : MonoBehaviour
{
    public Slider shirtsSlider;
    public Slider pantsSlider;

    public SpriteRenderer shirt;
    public SpriteRenderer leftLeg;
    public SpriteRenderer rightLeg;

    public Image inventoryShirt;
    public Image inventoryPants;

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

        shirtsSlider.value = Random.Range(0f, 1f);
        pantsSlider.value = Random.Range(0f, 1f);
    }

    void Update()
    {
        shirtHue = shirtsSlider.value;
        pantsHue = pantsSlider.value;

        shirt.color = Color.HSVToRGB(shirtHue, saturation, brightness);
        leftLeg.color = Color.HSVToRGB(pantsHue, saturation, brightness);
        rightLeg.color = Color.HSVToRGB(pantsHue, saturation, brightness);

        inventoryShirt.color = Color.HSVToRGB(shirtHue, saturation, brightness);
        inventoryPants.color = Color.HSVToRGB(pantsHue, saturation, brightness);
    }
}
