using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    private bool minimapOpen;
    public GameObject minimap;
    public TextMeshProUGUI minimapText;
    public GameObject shipTracker;
    public GameObject ship;
    public Transform mapCenter;
    public float worldRadius;
    public float mapRadius;

    // Start is called before the first frame update
    void Start()
    {
        minimap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            minimapOpen = !minimapOpen;
        }

        minimap.SetActive(minimapOpen);
        shipTracker.SetActive(minimapOpen);

        if (minimapOpen)
        {
            minimapText.text = "Press Q to close map";
        }
        else minimapText.text = "Press Q to open map";

        //set ship tracker to proper location
        Vector3 offset = (ship.transform.position / worldRadius) * mapRadius;
        shipTracker.transform.position = mapCenter.position + offset;
    }
}
