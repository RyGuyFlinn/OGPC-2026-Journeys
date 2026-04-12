using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPlaceHolder;

    [SerializeField] private GameObject slotHolder;
    [SerializeField] private ItemClass itemToAdd;
    [SerializeField] private ItemClass itemToRemove;

    [SerializeField] private SlotClass[] startingItems;

    public GameObject inventoryPanel;

    [Header("Abilities")]
    public float bombLaunchForce = 10f;
    public GameObject bombPrefab;

    public GameObject shieldPrefab;
    public float shieldCooldownTime;
    public float shieldUseTime;
    private GameObject spawnedShield = null;
    private float shieldCooldown = 0;
    private bool shieldAvalible = true;

    [Space]
    public GameObject hands;
    
    private SlotClass[] items;

    private GameObject[] slots;

    public SlotClass movingSlot;
    public SlotClass lastSelectedItem;
    public SlotClass originalSlot;
    private SlotClass tempSlot;
    bool isMovingItem;

    private bool isOpen = false;
    public bool CanOpen = true;
    private GameObject spawnedObject = null;

    [Header("Sprites")]
    public Sprite Basic;
    public Sprite Sword;
    public Sprite Bomb;
    public Sprite Upgrade;
    public void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];
        items = new SlotClass[slots.Length];

        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new SlotClass();
        }

        for (int i = 0; i < startingItems.Length; i++)
        {
            items[i] = startingItems[i];
        }

        for (int i = 0; i < slotHolder.transform.childCount; i++)
            slots[i] = slotHolder.transform.GetChild(i).gameObject;

        RefreshUI();

        turnOn();
    }
    
    private void Update()
    {

        if (PlayerManager.IsOnIsland)
        {
            ShowItems();
            AbilitiesManager();
            UpgradesManager();
            UpdateSlotsSprites();
        }

        if (!isOpen)
        {
            itemPlaceHolder.SetActive(isMovingItem);
            itemPlaceHolder.transform.position = Input.mousePosition;
            if (isMovingItem)
                itemPlaceHolder.GetComponent<Image>().sprite = movingSlot.GetItem().itemIcon;

            if (Input.GetMouseButtonDown(0)) //We Clicked!
            {
                //Find The closest slot for the slot we clicked on
                if (isMovingItem)
                    EndItemMove();
                else
                    BeginItemMove();
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) && CanOpen)
        {
            if (isOpen)
                turnOff();
            else
                turnOn();
        }
    }
    private void UpdateSlotsSprites()
    {
        if (items[15].GetItem() != null)
        {
            slots[15].GetComponent<Image>().sprite = Basic;
        }
        else slots[15].GetComponent<Image>().sprite = Sword;

        if (items[16].GetItem() != null)
        {
            slots[16].GetComponent<Image>().sprite = Basic;
        }
        else slots[16].GetComponent<Image>().sprite = Bomb;

        if (items[17].GetItem() != null)
        {
            slots[17].GetComponent<Image>().sprite = Basic;
        }
        else slots[17].GetComponent<Image>().sprite = Upgrade;
    }
    #region Showing Player Items
    private void ShowItems()
    {
        // Take the weopon in the main weopon slot and spawn its prefab in the scene.
        if (items[15].GetItem() != null && (spawnedObject == null || items[15].GetItem().itemName != spawnedObject.GetComponent<HoldingItem>().name))
        {
            if (spawnedObject != null)
            {
                Destroy(spawnedObject.gameObject);
            }

            GameObject holdingObject = items[15].GetItem().holdingObject;
            GameObject player = GameObject.Find("Player");

            spawnedObject = Instantiate(holdingObject, player.transform.position, Quaternion.identity);
            spawnedObject.transform.parent = transform.parent;
        }

        if (items[15].GetItem() == null && spawnedObject != null)
        {
            Destroy(spawnedObject.gameObject);
            spawnedObject = null;
        }

        //if holding no weapon, the player has two hands free
        hands.SetActive(items[15].GetItem() == null);
    }

    private void AbilitiesManager()
    {
        GameObject player = GameObject.Find("Player");

        if (items[16].GetItem() != null)
        {
            if (items[16].GetItem().itemName == "Bomb")
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    Vector3 mousePos = Input.mousePosition;

                    Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
                    worldMousePos.z = 0f;

                    Vector2 direction = (Vector2)worldMousePos - (Vector2)player.transform.position;

                    GameObject bomb = Instantiate(bombPrefab, player.transform.position, transform.rotation);

                    //ForceMode2D.Impulse
                    float dir = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    bomb.transform.rotation = Quaternion.AngleAxis(dir - 90f, Vector3.forward);
                    bomb.GetComponent<Rigidbody2D>().AddForce(bomb.transform.up * 300);
                }
            }
            if (items[16].GetItem().itemName == "Shield")
            {
                if (Input.GetKey(KeyCode.Q) && shieldAvalible)
                {
                    if (spawnedShield == null)
                    {
                        spawnedShield = Instantiate(shieldPrefab, player.transform.position, Quaternion.identity);
                    }
                    player.GetComponent<PlayerHealth>().hasShieldEquipped = true;
                    shieldCooldown += 1;
                    if (shieldCooldown > shieldUseTime)
                    {
                        shieldAvalible = false;
                        shieldCooldown = shieldCooldownTime;
                    }
                }
                else
                {
                    spawnedShield = null;
                    player.GetComponent<PlayerHealth>().hasShieldEquipped = false;
                }

                if (!shieldAvalible)
                {
                    shieldCooldown -= 1;
                    if (shieldCooldown <= 0)
                    {
                        shieldAvalible = true;
                        shieldCooldown = 0;
                    }
                }
            }
        }
    }
    private void UpgradesManager()
    {
        GameObject player = GameObject.Find("Player");
        
        if (items[17].GetItem() != null && items[17].GetItem().IsUpgrade == true)
        {
            if (items[17].GetItem().itemName == "Dash Upgrade")
            {
                player.GetComponent<PlayerMovement>().DashAbility = true;
            }
            else player.GetComponent<PlayerMovement>().DashAbility = false;
            if (items[17].GetItem().itemName == "Health Upgrade")
            {
                player.GetComponent<PlayerHealth>().HasExtraHealth = true;
                
            }
            else player.GetComponent<PlayerHealth>().HasExtraHealth = false;

        }
        else
            {
                player.GetComponent<PlayerMovement>().DashAbility = false;
                player.GetComponent<PlayerHealth>().HasExtraHealth = false;
            
            }
        }

    #endregion

    #region Inventory Utils
    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                if (items[i].GetItem().maxStack > 1)
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = items[i].GetQuantity() + "";
                else
                    slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";

            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                slots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }

    public bool Add(ItemClass item, int quantity = 1)
    {
        //items.Add(item);
        //check if inventory contains item

        SlotClass slot = Contains(item);

        if (slot != null && (slot.GetQuantity() + quantity) < slot.GetItem().maxStack)
            slot.AddQuantity(1);
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].GetItem() == null)
                {
                    items[i].AddItem(item, quantity);
                    break;

                }
            }
        }

        RefreshUI();
        return true;
    }

    public bool Remove(ItemClass item)
    {
        //items.Remove(item);
        SlotClass temp = Contains(item);

        if (temp != null)
        {
            if (temp.GetQuantity() > 1)
                temp.SubQuantity(1);
            else
            {
                int slotToRemoveIndex = 0;
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].GetItem() == item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }

                items[slotToRemoveIndex].Clear();
            }
        }
        else
        {
            return false;
        }

        RefreshUI();
        return true;
    }

    public void DropItem()
    {
        if (lastSelectedItem != null)
        {
            GameObject ObjectToSpawn = lastSelectedItem.GetItem().GroundObject;
            GameObject player = GameObject.Find("Player");

            Instantiate(ObjectToSpawn, player.transform.position, ObjectToSpawn.transform.rotation);

            Remove(lastSelectedItem.GetItem());
            lastSelectedItem = null;
        }
    }

    public SlotClass Contains(ItemClass item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetItem() == item)
                return items[i];
        }

        return null;
    }
    #endregion Inventory Utils

    #region Moving Stuff
    private bool BeginItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null || originalSlot.GetItem() == null)
            return false;

        movingSlot = new SlotClass(originalSlot);
        lastSelectedItem = new SlotClass(movingSlot);
        originalSlot.Clear();
        isMovingItem = true;

        RefreshUI();

        return true;
    }

    private bool EndItemMove()
    {
        originalSlot = GetClosestSlot();
        if (originalSlot == null)
        {
            Add(movingSlot.GetItem(), movingSlot.GetQuantity());
            movingSlot.Clear();
        }
        else
        {
            if (originalSlot.GetItem() != null)
            {
                if (originalSlot.GetItem() == movingSlot.GetItem()) //Stack Item
                {
                    if (originalSlot.GetQuantity() + movingSlot.GetQuantity() < originalSlot.GetItem().maxStack)
                    {
                        originalSlot.AddQuantity(movingSlot.GetQuantity());
                        movingSlot.Clear();
                    }
                    else
                        return false;
                }
                else //Swap Item
                {
                    tempSlot = new SlotClass(originalSlot); //a == b
                    originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity()); // b = c
                    movingSlot.AddItem(tempSlot.GetItem(), tempSlot.GetQuantity()); //c == a

                    RefreshUI();
                    return true;
                }
            }
            else /// place Item
            {
                originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
                movingSlot.Clear();
            }
        }

        isMovingItem = false;
        RefreshUI();
        return true;
    }

    private SlotClass GetClosestSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Vector2.Distance(slots[i].transform.position, Input.mousePosition) <= 32)
            {
                return items[i];
            }
        }

        return null;
    }
    #endregion Moving Stuff

    #region Turn On/Off
    public void turnOff()
    {
        Time.timeScale = 0f;
        inventoryPanel.SetActive(true);
        isOpen = false;
    }

    public void turnOn()
    {
        Time.timeScale = 1.0f;
        inventoryPanel.SetActive(false);
        isOpen = true;
    }
    #endregion Turn On/Off
}
