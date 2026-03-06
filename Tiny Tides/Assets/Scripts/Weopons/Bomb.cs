using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject sparks;
    public GameObject smoke;

    void Start()
    {
        StartCoroutine(ExplodeTimer());
    }

    IEnumerator ExplodeTimer()
    {
        yield return new WaitForSeconds(2);

        sparks.SetActive(true);
        smoke.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = true;

        yield return new WaitForSeconds(1.8f);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }
}
