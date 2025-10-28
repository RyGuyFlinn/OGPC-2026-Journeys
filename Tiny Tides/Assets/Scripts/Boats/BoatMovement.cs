using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float acceleration = 8f;
    public float maxSpeed = 5f;
    public float rotationSpeed = 180f;
    public float stopSmoothness = 3f;

    [Header("Cannon Settings")]
    public GameObject cannonballPrefab;
    public GameObject explosionPrefab;
    public Transform[] rightCannons;
    public Transform[] leftCannons;
    public float cannonballSpeed = 10f;
    public float fireCooldown = 0.5f;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip fireSFX;

    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;
    private float currentSpeed;

    private float lastFireTimeRight;
    private float lastFireTimeLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.drag = 0f;
        rb.angularDrag = 0f;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetMouseButtonDown(1) && Time.time >= lastFireTimeRight + fireCooldown)
        {
            StartCoroutine(FireCannons(rightCannons, true));
        }

        if (Input.GetMouseButtonDown(0) && Time.time >= lastFireTimeLeft + fireCooldown)
        {
            StartCoroutine(FireCannons(leftCannons, false));
        }
    }

    void FixedUpdate()
    {
        // Movement
        if (moveInput != 0)
            currentSpeed = Mathf.MoveTowards(currentSpeed, moveInput * maxSpeed, acceleration * Time.fixedDeltaTime);
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, stopSmoothness * Time.fixedDeltaTime);

        rb.velocity = transform.up * currentSpeed;

        // Rotation
        if (currentSpeed != 0 && turnInput != 0)
        {
            float rotationAmount = -turnInput * rotationSpeed * Time.fixedDeltaTime;
            rb.MoveRotation(rb.rotation + rotationAmount);
        }
    }

    private IEnumerator FireCannons(Transform[] cannons, bool isRightSide)
    {
        // Create a temporary list to shuffle
        List<Transform> shuffledCannons = new List<Transform>(cannons);

        // Fisher-Yates shuffle
        for (int i = shuffledCannons.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Transform temp = shuffledCannons[i];
            shuffledCannons[i] = shuffledCannons[j];
            shuffledCannons[j] = temp;
        }

        // Fire each cannon in shuffled order
        foreach (Transform cannon in shuffledCannons)
        {
            yield return new WaitForSeconds(Random.Range(0f, 0.05f));

            GameObject explosion = Instantiate(explosionPrefab, cannon.position, cannon.rotation);

            GameObject ball = Instantiate(cannonballPrefab, cannon.position, cannon.rotation);
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            ballRb.velocity = cannon.right * cannonballSpeed;

            FireSFX();

            yield return new WaitForSeconds(0.1f);
        }

        // Update cooldown after all cannonballs are fired
        if (isRightSide)
            lastFireTimeRight = Time.time;
        else
            lastFireTimeLeft = Time.time;
    }

    void FireSFX()
    {
        audioSource.pitch = 1f + Random.Range(-.7f, -.3f);
        audioSource.PlayOneShot(fireSFX);
    }
}
