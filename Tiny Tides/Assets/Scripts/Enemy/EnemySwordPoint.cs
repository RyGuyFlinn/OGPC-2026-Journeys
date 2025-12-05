using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySwordPoint : MonoBehaviour
{
    public Vector2 offset;
    public float controllerCursorSpeed = 900f;

    private Camera mainCamera;
    public GameObject enemy;
    private GameObject player;

    private Vector2 pointerInput;          // right stick input
    private Vector2 virtualCursor;         // fake mouse position
    private bool usingController = false;  // switches modes

    PlayerControls controls;

    void Awake()
    {
        
        controls = new PlayerControls();

        controls.GamePlay.Pointer.performed += ctx =>
        {
            pointerInput = ctx.ReadValue<Vector2>();
            if (pointerInput.magnitude > 0.1f)
                usingController = true;
        };

        controls.GamePlay.Pointer.canceled += ctx =>
        {
            pointerInput = Vector2.zero;
        };
        
    }

    void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.Find("Player");

        // Start virtual cursor at real mouse pos
        virtualCursor = Mouse.current.position.ReadValue();
    }

    void Update()
    {
        // Follow player position
        transform.position = enemy.transform.position + new Vector3(offset.x, offset.y, 0);

        if (Mouse.current.delta.ReadValue().magnitude > 0.01f)
        {
            usingController = false;
        }

        if (usingController)
        {
            virtualCursor += pointerInput * controllerCursorSpeed * Time.deltaTime;

            // Clamp to screen
            virtualCursor.x = Mathf.Clamp(virtualCursor.x, 0, Screen.width);
            virtualCursor.y = Mathf.Clamp(virtualCursor.y, 0, Screen.height);
        }
        else
        {
            virtualCursor = Mouse.current.position.ReadValue();
        }

        // Convert virtual cursor to world space
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(virtualCursor);
        worldPos.z = 0;

        // Aim sword
        Vector3 dir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
