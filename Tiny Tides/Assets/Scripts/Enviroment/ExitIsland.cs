using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitIsland : MonoBehaviour
{
    public GameObject player;
    public GameObject minimap;
    public GameObject playerBoat;
    public GameObject buttonPrompt;

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
        buttonPrompt.SetActive(PlayerInRange);

        if (PlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Player exiting island");
                player.SetActive(false);
                playerBoat.SetActive(true);
                PlayerManager.IsOnIsland = false;
                minimap.SetActive(true);
            }
        }
    }
}
