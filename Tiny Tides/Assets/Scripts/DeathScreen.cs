using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class DeathScreen : MonoBehaviour
{
    public TreasureData treasureData;
    public TextMeshProUGUI treasureText;

    void Start()
    {
        treasureText.text = "With " + treasureData.GetTreasure().ToString() + " treasure";
    }
    public void OnRetryButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
