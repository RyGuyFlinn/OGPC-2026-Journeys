using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private bool isActive = false;
    private GameObject Inventory;

    void Start()
    {
        InventoryManager manager = Object.FindFirstObjectByType<InventoryManager>();
        Inventory = manager.gameObject;
        gameObject.SetActive(false);
    }

    public void ToggleVisibility()
    {
        isActive = !isActive;
        gameObject.SetActive(isActive);

        if (isActive)
        {
            Inventory.GetComponent<InventoryManager>().CanOpen = false;
        }
        else
        {
            Inventory.GetComponent<InventoryManager>().CanOpen = true;
        }
    }
}
