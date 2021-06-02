using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
   public Animator animator;
    Rigidbody2D rid;


    const string IDLE = "SamratIdleAnim";
    const string WPUNCH = "SamratNormalPunch";
    const string HPUNCH = "SamratMultiPunch";
    const string KICK = "SamratPushKick";
    const string POWERUP = "SamratPowerUp";
    const string BEAM = "SamratKuganBeam";
    const string DIE = "SamratDie";
    const string RUN = "SamratRunAnim";
    const string UPPER = "SamratUpperCut";
    const string AKICK = "SamratAirKick";
    const string APUNCH = "SamratAirPunch";
    const string REVIVE = "SamratRevive";

    public float heavyPunchRequiretedStamina;
    public float upperRequiredStamina;
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
        if(PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.revive)
        {
            return;
        }

        if (PlayerManager.Manager.hp <= 0)
        {
            animator.Play(DIE);
        }
        if (PlayerManager.Manager.onDialogue || PlayerManager.Manager.isStaggered)
        { animator.Play(IDLE); return; }


        // Ground check

        if (PlayerManager.Manager.isGrounded)
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
            // Prevent downX from being used repeatedly
            // allow moves to be used repeteadly only when the player is not using downX
            // Take away the player's stamina by 5 when translate from strong move to weak move 

            if (PlayerManager.Manager.currentPlayerAnimationState != PlayerManager.playerAnimationState.downX && PlayerManager.Manager.currentPlayerAnimationState != PlayerManager.playerAnimationState.beam)
            {
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    // Down attacks go here

                    if (PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.normalX)
                    {
                        // transition from heavy to weak
                        if (PlayerManager.Manager.stamina >= 5)
                        {
                            if (Input.GetKeyDown(KeyCode.Z))
                            {
                                PlayerManager.Manager.stamina -= 5;
                                animator.Play(KICK);
                            }
                            else if (Input.GetKeyDown(KeyCode.C) && PlayerManager.Manager.stamina >= powerUpRequiredStamina)
                            {
                                PlayerManager.Manager.stamina -= 5;
                                animator.Play(POWERUP);
                            }
                            else if (Input.GetKeyDown(KeyCode.X) && PlayerManager.Manager.stamina >= upperRequiredStamina)
                            {
                                PlayerManager.Manager.stamina -= 5;
                                animator.Play(UPPER);
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Z))
                        {
                            animator.Play(KICK);
                        }
                        else if (Input.GetKeyDown(KeyCode.C) && PlayerManager.Manager.stamina >= powerUpRequiredStamina)
                        {
                            animator.Play(POWERUP);
                        }
                        else if (Input.GetKeyDown(KeyCode.X) && PlayerManager.Manager.stamina >= upperRequiredStamina)
                        {
                            animator.Play(UPPER);
                        }
                    }
                }
                else
                {
                    // normal attacks go here
                    if (PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.normalX)
                    {
                        // transition from heavy to weak
                        if (PlayerManager.Manager.stamina >= 5)
                        {
                            if (Input.GetKeyDown(KeyCode.Z))
                            {
                                PlayerManager.Manager.stamina -= 5;
                                animator.Play(WPUNCH);
                            }
                            else if (Input.GetKeyDown(KeyCode.C) && PlayerManager.Manager.stamina >= kuganBeamRequiredStamina)
                            {
                                PlayerManager.Manager.stamina -= 5 + kuganBeamRequiredStamina;
                                animator.Play(BEAM);
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Z))
                        {
                            animator.Play(WPUNCH);
                        }
                        else if (Input.GetKeyDown(KeyCode.X) && PlayerManager.Manager.stamina >= heavyPunchRequiretedStamina)
                        {
                            animator.Play(HPUNCH);
                        }
                        else if (Input.GetKeyDown(KeyCode.C) && PlayerManager.Manager.stamina >= kuganBeamRequiredStamina)
                        {
                            PlayerManager.Manager.stamina -= kuganBeamRequiredStamina;
                            animator.Play(BEAM);
                        }
                    }
                }
            }
        }
        else // AirMove
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                animator.Play(APUNCH);
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                animator.Play(AKICK);
            }

           if(PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.airKick || PlayerManager.Manager.currentPlayerAnimationState == PlayerManager.playerAnimationState.airPunch)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                    animator.Play(IDLE);
            }



        }
    }


    public void PlayReviveAnimation()
    {
        animator.Play(REVIVE);
        PlayerManager.Manager.currentPlayerAnimationState = PlayerManager.playerAnimationState.revive;
        Debug.Log("Revive");
    }
}
