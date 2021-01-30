using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D ridBody;
    BoxCollider2D boxCol;
    int moveValue;
   public float staminaRefillValue;


    // Ground
    public LayerMask groundLayer;
    public Vector2 boxSize;
    Vector2 pos;
    // Ground

    // Wall
    public LayerMask wallLayer;
    Vector2[] wallPos;
    public Vector2 wallBoxSize;
    // Wall


    bool isFacingRight;

    [SerializeField]
    bool showGizmo;
    // Cam
    private void Awake()
    {
        ridBody = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        PlayerManager.Manager.playerCamera.transform.position = this.gameObject.transform.position;
        PlayerManager.Manager.player = this.gameObject;
        
        
    }

    private void OnDestroy()
    {
        PlayerManager.Manager.player = null;
    }
    void Start()
    {
        isFacingRight = true;
        wallPos = new Vector2[2];
        PlayerManager.Manager.gravity = ridBody.gravityScale;
        PlayerManager.Manager.LookForThePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        CheckCondition();
        ApplyPlayerInfo();
        RefillStamina();
        // temp

    


    }

    private void FixedUpdate()
    {
        MovePlayer();
        StaggerPlayer();
    }

    private void LateUpdate()
    {
        PushBackPlayer();
    }


    void MovePlayer()
    {
        ridBody.velocity = new Vector2(moveValue * PlayerManager.Manager.speed, ridBody.velocity.y);


        if (!PlayerManager.Manager.isGrounded)
        {
                ridBody.gravityScale = PlayerManager.Manager.gravity;
            
        }
        else
            ridBody.gravityScale = 0;

    }

    void StaggerPlayer()
    {
        if(PlayerManager.Manager.isStaggered)
        {

            ridBody.velocity = PlayerManager.Manager.staggerDirection * PlayerManager.Manager.speed;
        }

    }
    void Jump()
    {
        ridBody.velocity = Vector2.up * PlayerManager.Manager.jumpHeight;
    }

    void CheckCondition()
    {
        pos = new Vector2(boxCol.bounds.center.x, boxCol.bounds.min.y);
        PlayerManager.Manager.isGrounded = Physics2D.OverlapBox(pos, boxSize, 0, groundLayer);

        // Wall Position

        // Left
        wallPos[0] = new Vector2(boxCol.bounds.min.x, boxCol.bounds.center.y);
        // Right
        wallPos[1] = new Vector2(boxCol.bounds.max.x, boxCol.bounds.center.y);

        bool leftWall =  Physics2D.OverlapBox(wallPos[0], wallBoxSize, 0, wallLayer);
        bool rightWall = Physics2D.OverlapBox(wallPos[1], wallBoxSize, 0, wallLayer);


        PlayerManager.Manager.isAgainstWall = (leftWall || rightWall) ? true : false;
    }

    void PushBackPlayer()
    {
        if(PlayerManager.Manager.isStaggered)
        {
            PlayerManager.Manager.staggerDuration -= 1 * Time.deltaTime;
        }

        if (PlayerManager.Manager.staggerDuration <= 0)
        {
            PlayerManager.Manager.staggerDuration = 0;
            PlayerManager.Manager.isStaggered = false;
        }

    }

    void GetInput()
    {
        if (!PlayerManager.Manager.canMove || PlayerManager.Manager.isStaggered)
        {
            PlayerManager.Manager.cameraTransposer.m_ScreenX = 0.5f;
            moveValue = 0;
          //  PlayerAnimationManager.Manager.currentState = PlayerAnimationManager.State.Idle;
            return;
        }


        if (Input.GetButton("Left"))
        {
            // Moving Left
            PlayerManager.Manager.cameraTransposer.m_ScreenX = 0.6f;
            moveValue = -1;
           // PlayerAnimationManager.Manager.currentState = PlayerAnimationManager.State.running;
            this.gameObject.transform.localScale = new Vector3(-Mathf.Abs(this.gameObject.transform.localScale.x), this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
            isFacingRight = false;
        }
        else if (Input.GetButton("Right"))
        {
            // Moving Right
            PlayerManager.Manager.cameraTransposer.m_ScreenX = 0.4f;
            moveValue = 1;
          //PlayerAnimationManager.Manager.currentState = PlayerAnimationManager.State.running;
            this.gameObject.transform.localScale = new Vector3(Mathf.Abs(this.gameObject.transform.localScale.x), this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
            isFacingRight = true;
        }
        else
        {
            // Idle
            PlayerManager.Manager.cameraTransposer.m_ScreenX = 0.5f;
            moveValue = 0;
         //   PlayerAnimationManager.Manager.currentState = PlayerAnimationManager.State.Idle;
        }

        // Jump system
        if (Input.GetButtonDown("Jump"))
        {
            if (PlayerManager.Manager.canJump)
            {
               
                if (PlayerManager.Manager.jumpCount > 0 && !PlayerManager.Manager.isAgainstWall)
                {
                    Jump();
                    PlayerManager.Manager.jumpCount--;
                }

                if(PlayerManager.Manager.isAgainstWall)
                {
                    if (PlayerManager.Manager.wallJumpCount > 0)
                    {
                        Jump();
                        PlayerManager.Manager.wallJumpCount--;
                    }
                    else if (PlayerManager.Manager.jumpCount > 0 )
                    {
                        Jump();
                        PlayerManager.Manager.jumpCount--;
                    }
                }

            }
        }

        // Attack

        if(Input.GetButton("Attack1"))
        {
        //    PlayerAnimationManager.Manager.currentCombatState = PlayerAnimationManager.Combat.attack;
        }



    }


    
    void RefillStamina()
    {
        if(PlayerManager.Manager.hp > 0)
        PlayerManager.Manager.stamina += staminaRefillValue * Time.deltaTime;
    }

    void ApplyPlayerInfo()
    {
        if (PlayerManager.Manager.isGrounded)
        {
            PlayerManager.Manager.jumpCount = PlayerManager.Manager.maxJumpCount;
            PlayerManager.Manager.wallJumpCount = PlayerManager.Manager.maxWallJumpCount;
        }


    }


    private void OnDrawGizmos()
    {
        if (!showGizmo)
        { return; }
        Gizmos.DrawCube(pos, boxSize);

        Gizmos.color = Color.red;

        Gizmos.DrawCube(wallPos[0], wallBoxSize);
        Gizmos.DrawCube(wallPos[1], wallBoxSize);
    }


    public void EnablePlayerMove()
    {
        PlayerManager.Manager.canMove = true;
    }
    public void DisablePlayerMove()
    {
        PlayerManager.Manager.canMove = false;
    }

    public void DecreaseStamina(float value)
    {
        PlayerManager.Manager.stamina -= value;
    }

    public void DeattachPlayer()
    {
        PlayerManager.Manager.player = null;
    }

    public void ShakeCamera(float percentage, float frequency, float intensity)
    {
        PlayerManager.Manager.cameraShakeIntensity = intensity;
        PlayerManager.Manager.cameraShakePercentage = percentage;
        PlayerManager.Manager.cameraShakeMaxFrequency = frequency;
    }

    public void CreateSoundEffect(AudioClip audioClip)
    {
        SoundManager.Manager.CrateSoundEffect(audioClip, this.gameObject.transform.position);
    }
}
