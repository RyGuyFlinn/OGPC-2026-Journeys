using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapIsland : MonoBehaviour
{
    public int spriteNum = 0;
    public Sprite[] islandSprites;
    public Sprite[] visitedSprites;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void SetRandomSprite()
    {
        spriteNum = Random.Range(0, islandSprites.Length);
        image.sprite = islandSprites[spriteNum];
    }

    public void SetVisited()
    {
        image.sprite = visitedSprites[spriteNum];
    }
}
