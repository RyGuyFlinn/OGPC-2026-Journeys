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
    public Animator LegsAnimator;
    public Animator TorsoAnimator;
    public Animator HeadAnimator;
    public Animator HatAnimator;
    public AudioClip DashSound;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerManager.CheatsActivated = true;
        }
        if (Input.GetKeyDown(KeyCode.H) && PlayerManager.CheatsActivated == true)
        {
            speed = 20f;
        }
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        if (moveInput.x < 0 && moveInput.y == 0)
        {
            LegsAnimator.SetBool("WalkLeft", true);
            LegsAnimator.SetBool("WalkRight", false);
            LegsAnimator.SetBool("WalkVertical", false);

            TorsoAnimator.SetBool("WalkLeft", true);
            TorsoAnimator.SetBool("WalkRight", false);
            TorsoAnimator.SetBool("WalkVertical", false);

            HeadAnimator.SetBool("WalkLeft", true);
            HeadAnimator.SetBool("WalkRight", false);
            HeadAnimator.SetBool("WalkVertical", false);

            HatAnimator.SetBool("WalkLeft", true);
            HatAnimator.SetBool("WalkRight", false);
            HatAnimator.SetBool("WalkVertical", false);
        }
        if (moveInput.x > 0 && moveInput.y == 0)
        {
            LegsAnimator.SetBool("WalkLeft", false);
            LegsAnimator.SetBool("WalkRight", true);
            LegsAnimator.SetBool("WalkVertical", false);

            TorsoAnimator.SetBool("WalkLeft", false);
            TorsoAnimator.SetBool("WalkRight", true);
            TorsoAnimator.SetBool("WalkVertical", false);

            HeadAnimator.SetBool("WalkLeft", false);
            HeadAnimator.SetBool("WalkRight", true);
            HeadAnimator.SetBool("WalkVertical", false);

            HatAnimator.SetBool("WalkLeft", false);
            HatAnimator.SetBool("WalkRight", true);
            HatAnimator.SetBool("WalkVertical", false);
        }
        if (moveInput.y != 0)
        {
            LegsAnimator.SetBool("WalkLeft", false);
            LegsAnimator.SetBool("WalkRight", false);
            LegsAnimator.SetBool("WalkVertical", true);

            TorsoAnimator.SetBool("WalkLeft", false);
            TorsoAnimator.SetBool("WalkRight", false);

            HeadAnimator.SetBool("WalkLeft", false);
            HeadAnimator.SetBool("WalkRight", false);

            HatAnimator.SetBool("WalkLeft", false);
            HatAnimator.SetBool("WalkRight", false);
            if (moveInput.y > 0)
            {
                TorsoAnimator.SetBool("WalkVertical", true);
                HeadAnimator.SetBool("WalkVertical", true);
                HatAnimator.SetBool("WalkVertical", true);
            }
            else
            { 
                TorsoAnimator.SetBool("WalkVertical", false);
                HeadAnimator.SetBool("WalkVertical", false);
                HatAnimator.SetBool("WalkVertical", false);
            }
        }
        if (moveInput.x == 0 && moveInput.y == 0)
        {
            LegsAnimator.SetBool("WalkLeft", false);
            LegsAnimator.SetBool("WalkRight", false);
            LegsAnimator.SetBool("WalkVertical", false);

            TorsoAnimator.SetBool("WalkLeft", false);
            TorsoAnimator.SetBool("WalkRight", false);
            TorsoAnimator.SetBool("WalkVertical", false);

            HeadAnimator.SetBool("WalkLeft", false);
            HeadAnimator.SetBool("WalkRight", false);
            HeadAnimator.SetBool("WalkVertical", false);

            HatAnimator.SetBool("WalkLeft", false);
            HatAnimator.SetBool("WalkRight", false);
            HatAnimator.SetBool("WalkVertical", false);
        }
        //Normalize input to prevent faster diagonal movement
        moveInput.Normalize();
        if (Input.GetKeyDown(KeyCode.Space) && DashAbility == true && !IsDashing && CanDash)
        {
            IsDashing = true;
            SoundFXManager.Instance.PlaySoundFXClip(DashSound, transform, 250f, 1f);
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
