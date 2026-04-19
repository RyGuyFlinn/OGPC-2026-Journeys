using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectsActiveOnCollision : MonoBehaviour
{
    public GameObject[] objects;
    public bool IsColliding = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsColliding = true;
            foreach (GameObject obj in objects)
            {
                if (obj != null) obj.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsColliding = false;
            foreach (GameObject obj in objects)
            {
                if (obj != null) obj.SetActive(false);
            }
        }
    }
}
