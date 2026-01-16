using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterIsland : MonoBehaviour
{
    public string sceneName = "";

    private bool PlayerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBoat")
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
                Debug.Log("Player enter island");
                LoadSceneByName();
            }
        }
    }

    public void LoadSceneByName()
    {
        SceneManager.LoadScene(sceneName);
    }
}
