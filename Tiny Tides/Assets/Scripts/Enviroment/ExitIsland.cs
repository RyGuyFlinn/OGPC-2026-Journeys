using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitIsland : MonoBehaviour
{
    private bool PlayerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player stay in island area");
            PlayerInRange = true;
        }
    }

    void Update()
    {
        if (PlayerInRange)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Player exiting island");
                LoadSceneByName();
            }
        }
    }

    public void LoadSceneByName()
    {
        SceneManager.LoadScene("WorldGeneration");
    }
}
