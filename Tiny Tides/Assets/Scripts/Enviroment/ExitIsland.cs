using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitIsland : MonoBehaviour
{
    public GameObject player;
    public GameObject playerBoat;

    private bool PlayerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerInRange = false;
        }
    }

    void Update()
    {
        if (PlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Player exiting island");
                player.SetActive(false);
                playerBoat.SetActive(true);
                PlayerManager.IsOnIsland = false;
            }
        }
    }
}
