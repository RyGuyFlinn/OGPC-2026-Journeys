using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySwordPoint : MonoBehaviour
{
    public Vector2 offset;
    public GameObject enemy;
    public GameObject player;
    public float rotationSpeed = 1f;
    public bool IsClub = false;
    private EnemyClub Enemyclub;
    [Header("Macaw Boss ONLY!!!!!")]
    public bool MacawBoss = false;
    public bool UsingSword = false;
    public bool UsingFlintLock = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (IsClub) Enemyclub = GetComponentInChildren<EnemyClub>();
    }

    void Update()
    {
        transform.position = enemy.transform.position + new Vector3(offset.x, offset.y, 0);


    Vector3 dir = player.transform.position - transform.position;
    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (MacawBoss == false)
        {
        if (IsClub == true && Enemyclub.Charge)
        {
            return;
        }
        else
        {   Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);


            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
      
        }
        }
        else
        {
            if (UsingSword == true){
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);


            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
            }
            else {
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);


            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation * Quaternion.Euler(0f, 0f, 180f),
                rotationSpeed * Time.deltaTime
                
            );
                
            }
        }
    }
}
