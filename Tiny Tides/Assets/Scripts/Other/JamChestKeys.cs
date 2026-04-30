using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JamChestKeys : MonoBehaviour
{
    public InventoryManager invman;
    public ItemClass key;
    public SetObjectsActiveOnCollision KeyIcons;
    public Sprite ChestOpen;
    private int keysleft = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //SlotClass slot = invman.Contains(key);
        if (Input.GetKeyDown(KeyCode.E) && KeyIcons.IsColliding == true && keysleft >= 0)
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
            transform.GetComponent<SpriteRenderer>().sprite = ChestOpen;
            Debug.Log("You WIN!!!");
            //This is the code to detect when you put all keys in the chest;

            SceneManager.LoadScene("EndCredits");
        }
    }
}
