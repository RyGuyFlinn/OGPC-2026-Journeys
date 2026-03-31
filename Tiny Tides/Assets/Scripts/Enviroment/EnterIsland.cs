using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterIsland : MonoBehaviour
{
    WorldGeneration manager = WorldGeneration.Instance;

    public int islandIndex = 1;

    private  GameObject islandPort;
    private GameObject player;
    private GameObject minimap;
    private GameObject playerBoat;
    public GameObject buttonPrompt;
    public GameObject mapIsland;

    private bool PlayerInRange = false;

    void Start()
    {
        player = manager.player;
        minimap = manager.minimap;
        playerBoat = manager.playerBoat;
        islandPort = GameObject.Find("Islands").transform.GetChild(islandIndex).gameObject;

        buttonPrompt.SetActive(false);
        playerBoat.SetActive(false);
        PlayerManager.IsOnIsland = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBoat")
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
        if (PlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Player enter island");

                player.SetActive(true);
                playerBoat.SetActive(false);
                PlayerManager.IsOnIsland = true;
                minimap.SetActive(false);

                //if this island is unvisited, reset all chests and enemies linked to it's port
                if (!mapIsland.GetComponent<MapIsland>().visited)
                {
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
                }
                mapIsland.GetComponent<MapIsland>().SetVisited();

                player.transform.position = islandPort.transform.position;
            }
        }
    }
}
