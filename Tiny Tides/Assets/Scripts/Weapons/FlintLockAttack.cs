using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlintLockAttack : MonoBehaviour
{
    public static FlintLockAttack instance;

    [Header("Attack Settings")]
    public float attackDelay = 0.1f;
    public int attackDamage = 1;

    [Header("References")]
    public Animator animator;

    [Space]
    public GameObject bulletPrefab;
    public GameObject muzzle;

    public AudioSource audioSource;
    public AudioClip shootSound;

    public bool attacking = false;
    public bool blocking = false;
    private bool canAttack = true;

    PlayerControls controls;

    void Awake()
    {
        instance = this;
        controls = new PlayerControls();

        controls.GamePlay.Attack.performed += ctx => callAttack();
    }

    private void callAttack()
    {
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;
        attacking = true;

        // Play SFX or animation if you have them
        if (audioSource && shootSound)
            audioSource.pitch = 1f + Random.Range(-0.3f, 0.3f);
            audioSource.PlayOneShot(shootSound);
            audioSource.pitch = 1f;

        if (animator) animator.SetTrigger("Shoot");
        
        Debug.Log("Shoot");

        GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, muzzle.transform.rotation);

        attacking = false;
     
        // Wait for cooldown before allowing next attack
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    void OnDisable()
    {
        controls.GamePlay.Disable();
    }
}
