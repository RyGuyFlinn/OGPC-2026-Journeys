using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBoatMovement : MonoBehaviour
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

    [Header("AI Settings")]
    public LayerMask boatLayer;
    private Transform currentTarget;
    public float attackDistance = 6f;
    public float chaseDistance = 12f;
    public float fleeDistance = 2f;
    private bool isAttacking = false;

    [Header("Wander Settings")]
    private Vector2 wanderTarget;
    private float wanderTimer = 0f;
    public float wanderChangeInterval = 2f;
    public float wanderRadius = 3f;
    public float wanderSpeed = 2f;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip fireSFX;

    [Header("Avoidance Settings")]
    public float avoidanceRadius = 2f;
    public float avoidanceStrength = 3f;

    private Rigidbody2D rb;
    private float currentSpeed;
    private float lastFireTimeRight;
    private float lastFireTimeLeft;
    private float fleeTimer;

    public enum States
    {
        IDLE,
        CHASING,
        ATTACKING,
        FLEEING
    }
    public States currentState = States.IDLE;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.drag = 0f;
        rb.angularDrag = 0.05f;

        PickRandomTarget();
    }

    void PickRandomTarget()
    {
        Collider2D[] boats = Physics2D.OverlapCircleAll(transform.position, 100f, boatLayer);
        if (boats.Length == 0) return;

        List<Collider2D> otherBoats = new List<Collider2D>();
        foreach (Collider2D b in boats)
            if (b.gameObject != gameObject)
                otherBoats.Add(b);

        if (otherBoats.Count == 0) return;

        currentTarget = otherBoats[Random.Range(0, otherBoats.Count)].transform;
    }

    void Update()
    {
        if (currentTarget != null)
            UpdateState();
        else
            PickRandomTarget();

        switch (currentState)
        {
            case States.IDLE:
                IdleBehavior();
                break;
            case States.CHASING:
                ChasePlayer();
                break;
            case States.ATTACKING:
                AttackBehavior();
                break;
            case States.FLEEING:
                FleeBehavior();
                break;
        }
    }

    void UpdateState()
    {
        float distance = Vector2.Distance(transform.position, currentTarget.position);

        if (distance <= fleeDistance)
        {
            currentState = States.FLEEING;
            fleeTimer = 2f;
        }
        else if (distance <= attackDistance)
        {
            currentState = States.ATTACKING;
        }
        else if (distance <= chaseDistance)
        {
            currentState = States.CHASING;
        }
        else
        {
            currentState = States.IDLE;
        }
    }

    Vector2 GetAvoidanceVector()
    {
        Vector2 avoidance = Vector2.zero;
        Collider2D[] nearbyBoats = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius, boatLayer);

        foreach (Collider2D boat in nearbyBoats)
        {
            if (boat.gameObject == gameObject) continue;

            Vector2 away = (Vector2)transform.position - (Vector2)boat.transform.position;
            float distance = away.magnitude;
            if (distance > 0)
                avoidance += away.normalized / distance; // closer boats push harder
        }

        return avoidance.normalized;
    }

    void IdleBehavior()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0f)
        {
            Vector2 randomOffset = Random.insideUnitCircle * wanderRadius;
            wanderTarget = (Vector2)transform.position + randomOffset;
            wanderTimer = wanderChangeInterval;
        }

        Vector2 toTarget = (wanderTarget - (Vector2)transform.position);
        Vector2 direction = toTarget.normalized;

        Vector2 avoidance = GetAvoidanceVector();
        if (avoidance != Vector2.zero)
            direction = (direction + avoidance * avoidanceStrength).normalized;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);

        float speedFactor = Mathf.Clamp(toTarget.magnitude / wanderRadius, 0.1f, 1f);
        currentSpeed = Mathf.Lerp(currentSpeed, wanderSpeed * speedFactor, acceleration * Time.deltaTime);
        rb.velocity = transform.up * currentSpeed;
    }

    void ChasePlayer()
    {
        if (currentTarget == null) return;

        Vector2 toAI = (transform.position - currentTarget.position).normalized;
        float sideDot = Vector2.Dot(toAI, currentTarget.right);
        Vector2 targetPos = currentTarget.position + (sideDot > 0 ? currentTarget.right : -currentTarget.right) * 3f;

        Vector2 directionToTarget = (targetPos - (Vector2)transform.position).normalized;

        Vector2 avoidance = GetAvoidanceVector();
        if (avoidance != Vector2.zero)
            directionToTarget = (directionToTarget + avoidance * avoidanceStrength).normalized;

        float targetAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;
        float angleDifference = Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle);

        float minTurnSpeed = maxSpeed * 0.3f;
        float desiredSpeed = Mathf.Abs(angleDifference) > 10f ? minTurnSpeed : maxSpeed;

        currentSpeed = Mathf.Lerp(currentSpeed, desiredSpeed, acceleration * Time.deltaTime);
        rb.velocity = transform.up * currentSpeed;

        float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    void AttackBehavior()
    {
        if (currentTarget == null || isAttacking) return;

        Vector2 toPlayer = (currentTarget.position - transform.position).normalized;
        float sideDot = Vector2.Dot(toPlayer, transform.right);
        float angleToSide = Mathf.Acos(Mathf.Clamp(Vector2.Dot(transform.right * Mathf.Sign(sideDot), toPlayer), -1f, 1f)) * Mathf.Rad2Deg;

        float maxAngle = 15f;

        if (angleToSide <= maxAngle)
        {
            if (sideDot > 0 && Time.time >= lastFireTimeRight + fireCooldown)
            {
                lastFireTimeRight = Time.time;
                StartCoroutine(FireCannons(rightCannons, true));
            }
            else if (sideDot < 0 && Time.time >= lastFireTimeLeft + fireCooldown)
            {
                lastFireTimeLeft = Time.time;
                StartCoroutine(FireCannons(leftCannons, false));
            }
        }

        ChasePlayer(); // keeps circling while firing
    }

    void FleeBehavior()
    {
        fleeTimer -= Time.deltaTime;

        Vector2 directionAway = (transform.position - currentTarget.position).normalized;

        Vector2 avoidance = GetAvoidanceVector();
        if (avoidance != Vector2.zero)
            directionAway = (directionAway + avoidance * avoidanceStrength).normalized;

        float targetAngle = Mathf.Atan2(directionAway.y, directionAway.x) * Mathf.Rad2Deg - 90f;
        float newAngle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);

        currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed * 0.7f, acceleration * Time.deltaTime);
        rb.velocity = transform.up * currentSpeed;

        if (fleeTimer <= 0f)
            currentState = States.IDLE;
    }

    private IEnumerator FireCannons(Transform[] cannons, bool isRightSide)
    {
        List<Transform> shuffledCannons = new List<Transform>(cannons);
        for (int i = shuffledCannons.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Transform temp = shuffledCannons[i];
            shuffledCannons[i] = shuffledCannons[j];
            shuffledCannons[j] = temp;
        }

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
    }

    void FireSFX()
    {
        audioSource.pitch = 1f + Random.Range(-.7f, -.3f);
        audioSource.PlayOneShot(fireSFX);
    }
}
