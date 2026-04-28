using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    public Animator LegsAnimator;
    public Animator TorsoAnimator;
    public Animator HeadAnimator;
    private Vector3 OldPosition;
    // Start is called before the first frame update
    void Start()
    {
        OldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != OldPosition)
        {
            if ((transform.position.x < OldPosition.x) && (Mathf.Abs(transform.position.x - OldPosition.x) > Mathf.Abs(transform.position.y - OldPosition.y)))
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
            }
            if ((transform.position.x > OldPosition.x) && (Mathf.Abs(transform.position.x - OldPosition.x) > Mathf.Abs(transform.position.y - OldPosition.y)))
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

            }
            if ((transform.position.y - OldPosition.y) != 0f && Mathf.Abs(transform.position.x - OldPosition.x) < Mathf.Abs(transform.position.y - OldPosition.y))
            {
                LegsAnimator.SetBool("WalkLeft", false);
                LegsAnimator.SetBool("WalkRight", false);
                LegsAnimator.SetBool("WalkVertical", true);

                TorsoAnimator.SetBool("WalkLeft", false);
                TorsoAnimator.SetBool("WalkRight", false);

                HeadAnimator.SetBool("WalkLeft", false);
                HeadAnimator.SetBool("WalkRight", false);


                if ((transform.position.y > OldPosition.y) && (Mathf.Abs(transform.position.x - OldPosition.x) < Mathf.Abs(transform.position.y - OldPosition.y)))
                {
                    TorsoAnimator.SetBool("WalkVertical", true);
                    HeadAnimator.SetBool("WalkVertical", true);

                }
                else
                {
                    TorsoAnimator.SetBool("WalkVertical", false);
                    HeadAnimator.SetBool("WalkVertical", false);

                }
            }
        }
            else
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

            }
        
        OldPosition = transform.position;
    }
}
