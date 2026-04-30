using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TreasureData treasureData;
    void Start()
    {
        treasureData.SetTreasure(0);
    }

    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("WorldGeneration");
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
