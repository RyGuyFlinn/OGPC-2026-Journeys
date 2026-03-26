using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public GameObject text;
    public ItemClass itemToAdd;

    private InventoryManager inventory;
    private bool playerInRange = false;
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<InventoryManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
            text.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
            text.SetActive(false);
        } 
    }

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.Add(itemToAdd, 1);

                Destroy(gameObject);
            }
        }
    }
}
