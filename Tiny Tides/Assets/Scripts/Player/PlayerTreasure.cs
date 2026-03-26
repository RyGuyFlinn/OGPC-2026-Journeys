using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTreasure : MonoBehaviour
{
    public TreasureData treasureData;
    public TextMeshProUGUI treasureText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        treasureText.text = "Treasure: " + treasureData.GetTreasure().ToString();
    }
}
