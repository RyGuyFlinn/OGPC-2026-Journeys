using System.Collections;
using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
    [Header("Item")]
    public string itemName;
    public string Description;
    public string Stats;
    public Sprite itemIcon;
    public int maxStack = 1;

    public bool isHoldable = false;
    public GameObject holdingObject;  
    public GameObject GroundObject;

    public abstract ItemClass GetItem();
    public abstract ToolClass GetTool();
    public abstract MiscClass GetMisc();
    public abstract ConsumableClass GetConsumable();
}
