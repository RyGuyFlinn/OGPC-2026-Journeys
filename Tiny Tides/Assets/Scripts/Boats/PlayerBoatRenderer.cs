using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoatRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] shipSprites;
    public GameObject boat;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        int spriteNum = (int)Mathf.Round(shipSprites.Length * (boat.transform.eulerAngles.z / 360)) % shipSprites.Length;
        spriteRenderer.sprite = shipSprites[spriteNum];
    }
}
