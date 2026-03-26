using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryButtonManager : MonoBehaviour
{
    public void OnQuitToMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
