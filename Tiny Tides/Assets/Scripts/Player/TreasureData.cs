using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "treasureData", menuName = "Custom/tresureData")]
public class TreasureData : ScriptableObject
{
    public static int treasure = 0;
    
    public int GetTreasure()
    {
        return treasure;
    }

    public static void ChangeTreasure(int treasureChange)
    {
        treasure += treasureChange;
    }

    public void SetTreasure(int treasureSet)
    {
        treasure = treasureSet;
    }
}
