using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject sparks;
    public GameObject smoke;
    public GameObject bombCollider;

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
        bombCollider.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        bombCollider.SetActive(false);
        yield return new WaitForSeconds(1.3f);
        Destroy(gameObject);
    }

    
}
