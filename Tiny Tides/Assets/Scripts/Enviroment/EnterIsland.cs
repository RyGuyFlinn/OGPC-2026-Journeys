using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterIsland : MonoBehaviour
{
    public GameObject islandPort;
    public GameObject player;
    public GameObject playerBoat;

    private bool PlayerInRange = false;

    void Start()
    {
        player = GameObject.Find("Player");
        playerBoat = GameObject.Find("PlayerBoat");
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

                //player.SetActive(true);
                playerBoat.SetActive(false);

                player.transform.position = islandPort.transform.position;
            }
        }
    }
}
