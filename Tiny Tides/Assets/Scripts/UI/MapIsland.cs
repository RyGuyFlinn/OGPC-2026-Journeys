using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapIsland : MonoBehaviour
{
    public int spriteNum = 0;
    public bool visited = false;
    public Sprite[] islandSprites;
    public Sprite[] visitedSprites;
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetRandomSprite()
    {
        spriteNum = Random.Range(0, islandSprites.Length);
        image.sprite = islandSprites[spriteNum];
        visited = false;
    }

    public void SetVisited()
    {
        image.sprite = visitedSprites[spriteNum];
        visited = true;
    }
}
