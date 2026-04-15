using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapIsland : MonoBehaviour
{
    public int spriteNum = 0;
    public bool visited = false;
    public Sprite[] islandSprites;
    public Sprite[] rockySprites;
    public Sprite[] glacierSprites;
    public Sprite[] visitedSprites;
    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetRandomSprite(int biomeNum)
    {
        spriteNum = Random.Range(0, islandSprites.Length);
        if (biomeNum == 0)
        {
            image.sprite = rockySprites[spriteNum];
        }
        else if (biomeNum == 1)
        {
            image.sprite = glacierSprites[spriteNum];
        }
        else
        {
            image.sprite = islandSprites[spriteNum];
        }
        visited = false;
    }

    public void SetVisited()
    {
        image.sprite = visitedSprites[spriteNum];
        visited = true;
    }
}
