using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySwordPoint : MonoBehaviour
{
    public Vector2 offset;
    public GameObject enemy;
    private GameObject player;
    public float rotationSpeed = 10f;
    void Start()
    {
        player = GameObject.Find("Player");

    }

    void Update()
    {
        transform.position = enemy.transform.position + new Vector3(offset.x, offset.y, 0);


    Vector3 dir = player.transform.position - transform.position;
    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    
  
    Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

  
    transform.rotation = Quaternion.RotateTowards(
        transform.rotation, 
        targetRotation, 
        rotationSpeed * Time.deltaTime
    );
    }
}
