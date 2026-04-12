using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Transform player;
    public GameObject InteractDisplay;
    public GameObject Chatbox;
    public GameObject AllUI;
    private bool Chatting = false;
    public string[] Dialogues;
    private int TalkPercent = 1;
    private GameObject Inventory;
    private bool FinishChatting = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        AllUI = GameObject.FindGameObjectWithTag("UI");
        InventoryManager manager = Object.FindFirstObjectByType<InventoryManager>();
        Inventory = manager.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float playerdistance = Vector3.Distance(player.position, transform.position);
        if ((playerdistance <= 3f))
        {
            InteractDisplay.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Chatbox = AllUI.GetComponentInChildren<NPCChatBoxData>(true).gameObject;
                Inventory.GetComponent<InventoryManager>().CanOpen = false;
                Chatting = true;
                Time.timeScale = 0f;
                Chatbox.SetActive(true);
                if (!FinishChatting)
                {  
                    TalkPercent = 1;
                    Chatbox.GetComponent<NPCChatBoxData>().text.text = Dialogues[0];
                }
                else 
                {
                    TalkPercent = Dialogues.Length;
                    Chatbox.GetComponent<NPCChatBoxData>().text.text = Dialogues[TalkPercent - 1];
                }
            }
        }
        else
        {
            InteractDisplay.SetActive(false);
        }
        if (Chatting) 
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (TalkPercent == Dialogues.Length)
                {
                    Chatbox = AllUI.GetComponentInChildren<NPCChatBoxData>(true).gameObject;
                    Inventory.GetComponent<InventoryManager>().CanOpen = true;
                    Time.timeScale = 1f;
                    Chatting = false;
                    FinishChatting = true;
                    Chatbox.SetActive(false);
                }
                else
                {
                    TalkPercent += 1;
                    Chatbox.GetComponent<NPCChatBoxData>().text.text = Dialogues[TalkPercent - 1];
                }
            }
        }
    }
}
