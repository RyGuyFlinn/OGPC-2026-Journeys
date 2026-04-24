using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public ItemClass itemToAdd;
    public int cost;

    public AudioClip success;
    public AudioClip denied;

    private InventoryManager inventory;
    
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }

    public void OnButtonPressed()
    {
        if (TreasureData.treasure >= cost)
        {
            TreasureData.ChangeTreasure(-cost);
            inventory.Add(itemToAdd, 1);
            
            SoundFXManager.Instance.PlaySoundFXClip(success, transform, 1, 1, false);
        }
        else
        {
            SoundFXManager.Instance.PlaySoundFXClip(denied, transform, 1, 1, false);
        }
    }
}
