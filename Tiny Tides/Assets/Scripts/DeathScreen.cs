using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class DeathScreen : MonoBehaviour
{
    public void OnRetryButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
