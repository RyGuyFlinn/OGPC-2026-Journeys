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

        Explode();
    }

    void Explode()
    {

        sparks.SetActive(true);
        smoke.SetActive(true);
        //Destroy(gameObject);
    }
}
