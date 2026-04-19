using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamChestKeys : MonoBehaviour
{
    public InventoryManager invman;
    public ItemClass key;
    public SetObjectsActiveOnCollision KeyIcons;
    private int keysleft = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SlotClass slot = invman.Contains(key);
        if (Input.GetKeyDown(KeyCode.E) && KeyIcons.IsColliding == true)
        {
            foreach (SlotClass items in invman.items)
            {
                if (items.GetItem() == key)
                {
                    invman.Remove(items.GetItem());
                    invman.RefreshUI();

                    Destroy(KeyIcons.objects[keysleft]);
                    keysleft--;
                    
                }
            }
        }
        if (keysleft < 0)
        {
            Debug.Log("You WIN!!!");
            //This is the code to detect when you put all keys in the chest;
        }
    }
}
