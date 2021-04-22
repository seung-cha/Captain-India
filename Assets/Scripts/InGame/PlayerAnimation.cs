using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rid;

    
    const string IDLE = "SamratIdleAnim";
    const string WPUNCH = "SamratNormalPunch";
    const string HPUNCH = "SamratMultiPunch";
    const string KICK = "SamratPushKick";
    const string POWERUP = "SamratPowerUp";
    const string BEAM = "SamratKuganBeam";
    const string DIE = "SamratDie";
    const string RUN = "SamratRunAnim";

    public float heavyPunchRequiretedStamina;
    public float powerUpRequiredStamina;
    public float kuganBeamRequiredStamina;
    void Start()
    {
        animator = GetComponent<Animator>();
        rid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerManager.Manager.hp <= 0)
        {
            animator.Play(DIE);
        }
        if (PlayerManager.Manager.onDialogue || PlayerManager.Manager.isStaggered)
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

            // Attack Part

            if (Input.GetKey(KeyCode.DownArrow))
            {
                // 2 Attacks
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    animator.Play(KICK);
                }

                if(Input.GetKeyDown(KeyCode.X) && PlayerManager.Manager.stamina >= powerUpRequiredStamina)
                {
                    animator.Play(POWERUP);
                }

            }
            else
            {
                // 5 Attacks
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    animator.Play(WPUNCH);
                }

                if(Input.GetKeyDown(KeyCode.X) && PlayerManager.Manager.stamina >= heavyPunchRequiretedStamina)
                {
                    animator.Play(HPUNCH);
                }

                if (PlayerManager.Manager.awakened)
                {
                    if (Input.GetKeyDown(KeyCode.C) && PlayerManager.Manager.stamina >= kuganBeamRequiredStamina)
                    {
                        animator.Play(BEAM);
                    }
                }
            }
           



        }
    }
}
