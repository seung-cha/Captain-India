using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D ridBody;
    BoxCollider2D boxCol;
    int moveValue;


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

    // Cam
    private void Awake()
    {
        ridBody = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        wallPos = new Vector2[2];
        PlayerManager.Manager.gravity = ridBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        CheckCondition();
        ApplyPlayerInfo();

        // temp

    


    }

    private void FixedUpdate()
    {
        MovePlayer();
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

        bool leftWall = Physics2D.OverlapBox(wallPos[0], wallBoxSize, 0, wallLayer);
        bool rightWall = Physics2D.OverlapBox(wallPos[1], wallBoxSize, 0, wallLayer);


        if (leftWall || rightWall)
        {
           PlayerManager.Manager.isAgainstWall = true; 
        }
        else
        {
            PlayerManager.Manager.isAgainstWall = false;
        }
    }

    void GetInput()
    {
        if (!PlayerManager.Manager.canMove)
        {
            PlayerManager.Manager.cameraTransposer.m_ScreenX = 0.5f;
            moveValue = 0;
            return;
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            PlayerManager.Manager.cameraTransposer.m_ScreenX = 0.6f;
            moveValue = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            PlayerManager.Manager.cameraTransposer.m_ScreenX = 0.4f;
            moveValue = 1;
        }
        else
        {
            PlayerManager.Manager.cameraTransposer.m_ScreenX = 0.5f;
            moveValue = 0;
        }

        // Jump system
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (PlayerManager.Manager.canJump)
            {
               
                if (PlayerManager.Manager.jumpCount > 0 && !PlayerManager.Manager.isAgainstWall)
                {
                    Jump();
                    PlayerManager.Manager.jumpCount--;
                }

                if(PlayerManager.Manager.isAgainstWall && PlayerManager.Manager.wallJumpCount > 0)
                {
                    Jump();
                    PlayerManager.Manager.wallJumpCount--;
                }

            }
        }





    }





    void ApplyPlayerInfo()
    {
        if (PlayerManager.Manager.isGrounded)
        {
            PlayerManager.Manager.jumpCount = PlayerManager.Manager.maxJumpCount;
            PlayerManager.Manager.wallJumpCount = PlayerManager.Manager.maxWallJumpCount;
        }


    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(pos, boxSize);


        /*
        Gizmos.color = Color.red;

        Gizmos.DrawCube(wallPos[0], wallBoxSize);
        Gizmos.DrawCube(wallPos[1], wallBoxSize);
        */
    }

}
