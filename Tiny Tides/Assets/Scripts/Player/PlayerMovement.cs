using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool DashAbility = false;
    public bool IsDashing = false;
    public float TimeDashing = 0f;
    public bool CanDash = true;
    private Vector2 LastInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        //Normalize input to prevent faster diagonal movement
        moveInput.Normalize();
        if (Input.GetKeyDown(KeyCode.Space) && DashAbility == true && !IsDashing && CanDash)
        {
            IsDashing = true;
            LastInput = moveInput;
            TimeDashing = 0f;
        }
        if (IsDashing)
        {
            if (TimeDashing < 0.2f)
            {
                rb.velocity = LastInput * speed * 3;
                TimeDashing += Time.deltaTime;
            }
            else
            {
                IsDashing = false;
                StartCoroutine(DashCooldown());
            }
        }
    }

    void FixedUpdate()
    {
        if (!IsDashing) rb.velocity = moveInput * speed;
    }
    private IEnumerator DashCooldown()
    {
        CanDash = false;
        yield return new WaitForSeconds(0.5f);
        CanDash = true;
    }
    
}
