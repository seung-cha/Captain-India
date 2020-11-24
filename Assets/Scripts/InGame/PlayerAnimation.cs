using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rid;

    const string IDLE = "SamratIdleAnim";
    const string PUNCH = "SamratPunchAnim";
    const string RUN = "SamratRunAnim";
    void Start()
    {
        animator = GetComponent<Animator>();
        rid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Manager.onDialogue)
        { animator.Play(IDLE);  return; }
        // Ground check


        if(PlayerManager.Manager.isGrounded)
        {
            // if able to move
            if (PlayerManager.Manager.canMove)
            {
                if (rid.velocity != Vector2.zero)
                    animator.Play(RUN);
                else
                    animator.Play(IDLE);

            }
            if (Input.GetKeyDown(KeyCode.Z))
                animator.Play(PUNCH);
        }


    }
}
