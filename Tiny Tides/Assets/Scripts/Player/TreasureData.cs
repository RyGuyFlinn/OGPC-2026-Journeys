using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "treasureData", menuName = "Custom/tresureData")]
public class TreasureData : ScriptableObject
{
    private int treasure = 0;
    public int GetTreasure()
    {
        return treasure;
    }

    public void ChangeTreasure(int treasureChange)
    {
        treasure += treasureChange;
    }

    public void SetTreasure(int treasureSet)
    {
        treasure = treasureSet;
    }
}
