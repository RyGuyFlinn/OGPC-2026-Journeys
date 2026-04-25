using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBoarding : MonoBehaviour
{
    private GameObject islandPort;
    private GameObject player;
    private GameObject minimap;
    private GameObject playerBoat;
    public GameObject buttonPrompt;

    public EnemyBoatHealth health;
    public bool canBoard = false;

    private bool PlayerInRange = false;

    public GameObject enemyBoat;

    WorldGeneration manager;

    void Start()
    {
        manager = WorldGeneration.Instance;

        player = manager.player;
        minimap = manager.minimap;
        playerBoat = manager.playerBoat;

        islandPort = GameObject.Find("Islands").transform.GetChild(11).gameObject;

        buttonPrompt.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBoat" && canBoard == true)
        {
            PlayerInRange = true;
            buttonPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerBoat")
        {
            PlayerInRange = false;
            buttonPrompt.SetActive(false);
        }
    }

    void Update()
    {
        Debug.Log(player);
        if (health.health <= 50)
        {
            canBoard = true;
        }
        else
        {
            canBoard = false;
        }

        if (PlayerInRange && canBoard)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Player Boarding");

                player.SetActive(true);
                playerBoat.SetActive(false);
                PlayerManager.IsOnIsland = true;
                minimap.SetActive(false);
    
                //Reset chests
                for (int i = 0; i < islandPort.GetComponent<ExitIsland>().chests.Length; i++)
                {
                    islandPort.GetComponent<ExitIsland>().chests[i].transform.GetChild(1).GetComponent<OpenChest>().ResetChest();
                }

                //Reset enemy spawners
                for (int i = 0; i < islandPort.GetComponent<ExitIsland>().enemies.Length; i++)
                {
                    islandPort.GetComponent<ExitIsland>().enemies[i].GetComponent<EnemySpawnPoint>().ResetSpawnPoint();
                }

                player.transform.GetChild(0).position = islandPort.transform.position;

                Destroy(enemyBoat);
            }
        }
    }
}
