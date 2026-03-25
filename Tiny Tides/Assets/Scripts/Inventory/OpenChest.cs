using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class OpenChest : MonoBehaviour
{
    public GameObject chest;
    private bool open = false;
    public Sprite closedChest;
    public Sprite openChest;
    public GameObject[] lootTable;
    public int lootNum;

    [Space]
    public GameObject textPrompt;
    public Sprite unlockedText;
    public Sprite lockedText;

    [Space]
    public EnemyDetector enemyDetector;

    private bool playerInRange;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer textRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = chest.GetComponent<SpriteRenderer>();
        textRenderer = textPrompt.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !open)
        {
            playerInRange = true;
            textPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
            textPrompt.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !enemyDetector.enemyInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Chest open");

                open = true;
            }
        }

        if (open)
        {
            spriteRenderer.sprite = openChest;
            textPrompt.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = closedChest;
        }

        if (enemyDetector.enemyInRange)
        {
            textRenderer.sprite = lockedText;
        }
        else
        {
            textRenderer.sprite = unlockedText;
        }
    }
}
