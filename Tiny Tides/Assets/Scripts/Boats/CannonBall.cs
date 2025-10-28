using System.Collections;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject explosionPrefab;
    public int damage = 10;
    private bool canExplode = false;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip fireSFX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Delay());
    }

    void Update()
    {
        if (rb.velocity.magnitude <= 1f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col) // Note: uppercase 'D'!
    {
        if (canExplode == true)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<PlayerBoatHealth>().health -= damage;
                StartCoroutine(ExplodeAndDestroy());
            }
            else if (col.CompareTag("Enemy"))
            {
                col.GetComponent<EnemyBoatHealth>().health -= damage;
                StartCoroutine(ExplodeAndDestroy());
            }
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.1f);
        canExplode = true;
    }

    private IEnumerator ExplodeAndDestroy()
    {
        FireSFX();
        GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.1f);
        Destroy(explosion);
        Destroy(gameObject);
    }

    void FireSFX()
    {
        audioSource.pitch = 1f + Random.Range(-0.3f, 0.3f);
        audioSource.PlayOneShot(fireSFX);
    }
}
