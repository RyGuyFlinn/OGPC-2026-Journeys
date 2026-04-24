using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarUI : MonoBehaviour
{
    public GameObject BossBar;
    public InventoryManager inventorymanager;
    private GameObject Boss;
    private bool bossSummoned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Boss = GameObject.FindGameObjectWithTag("Boss");
        if (Boss != null){
            bossSummoned = true;
        }
        if (inventorymanager.isOpen == false){
            BossBar.SetActive(false);
        }
        else if (bossSummoned == true){
            BossBar.SetActive(true);
        }
    }
}
