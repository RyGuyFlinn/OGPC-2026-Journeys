using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ItemDescriptionDisplay : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public GameObject ItemIcon;
    private Image DisplayImage;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Stats;
    public InventoryManager Inventory;
    // Start is called before the first frame update
    void Start()
    {
        DisplayImage = ItemIcon.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.movingSlot.GetItem() != null)
        {
            Name.text = Inventory.movingSlot.GetItem().itemName;
            Description.text = Inventory.movingSlot.GetItem().Description;
            Stats.text = Inventory.movingSlot.GetItem().Stats;
            DisplayImage.sprite = Inventory.movingSlot.GetItem().itemIcon;
        }
    }
}
