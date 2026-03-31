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
    public new AudioClip unlockAudio;
    public GameObject[] lootTable;
    public int lootNum;
    private float itemOffset = 2;

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

    public void ResetChest()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && !enemyDetector.enemyInRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && !open)
            {
                Debug.Log("Chest open");

                SoundFXManager.Instance.PlaySoundFXClip(unlockAudio, transform, 1, 1, false);
                open = true;

                //when the chest is opened, spawn in random items nearby
                for (int i = 0; i < lootNum; i++)
                {
                    Vector3 randOffset = new Vector3(
                        Random.Range(-itemOffset, itemOffset), 
                        Random.Range(-itemOffset, itemOffset), 
                        0);

                    Instantiate(lootTable[Random.Range(0, lootTable.Length)], 
                        transform.position + randOffset, Quaternion.identity);
                }
            }
        }

        //set the chests sprite to open or closed
        if (open)
        {
            spriteRenderer.sprite = openChest;
            textPrompt.SetActive(false);
        }
        else
        {
            spriteRenderer.sprite = closedChest;
        }

        //set the text sprite to indicate whether the chest is locked or unlocked
        if (enemyDetector.enemyInRange)
        {
            textRenderer.sprite = lockedText;
        }
        else
        {
            textRenderer.sprite = unlockedText;
        }

        //when the player leaves the island, reset all chests
        //if (PlayerManager.IsOnIsland == false)
        //{
        //    ResetChest();
        //}
    }
}
