using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private bool isActive = false;
    private GameObject Inventory;
    public GameObject panel;

    void Start()
    {
        InventoryManager manager = Object.FindFirstObjectByType<InventoryManager>();
        Inventory = manager.gameObject;

        panel.SetActive(false);
    }

    public void ToggleVisibility()
    {
        isActive = !isActive;

        var invManager = Inventory.GetComponent<InventoryManager>();

        if (isActive)
        {
            Time.timeScale = 0f;
            invManager.CanOpen = false;
        }
        else
        {
            Time.timeScale = 1f;
            invManager.CanOpen = true;
        }

        panel.SetActive(isActive);
    }
}
