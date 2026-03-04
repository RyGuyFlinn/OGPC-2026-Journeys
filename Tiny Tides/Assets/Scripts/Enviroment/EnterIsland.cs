using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterIsland : MonoBehaviour
{
    WorldGeneration manager = WorldGeneration.Instance;

    private  GameObject islandPort;
    private GameObject player;
    private GameObject playerBoat;

    private bool PlayerInRange = false;

    void Start()
    {
        player = manager.player;
        playerBoat = manager.playerBoat;
        islandPort = GameObject.Find("RedMainIsland").transform.GetChild(1).gameObject;

        player.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBoat")
        {
            PlayerInRange = true;
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

                player.transform.position = islandPort.transform.position;
            }
        }
    }
}
