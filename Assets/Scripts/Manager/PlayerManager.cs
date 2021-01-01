using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Manager;

    public int hp;
    public float stamina;

    public float speed;
    public float gravity;
    public int jumpCount;
    public int wallJumpCount;

    public int maxHealth;
    public float maxStamina;

    public float jumpHeight;
    public bool canJump;
    public bool isGrounded;
    public bool isAgainstWall;
    public bool canMove;
    public bool onDialogue;

    public bool isStaggered;
    public float staggerDuration;
    public Vector2 staggerDirection;

    public GameObject player;

    public int maxJumpCount
    { get; private set; }

    public int maxWallJumpCount
    { get; private set; }


    [SerializeField]
    private int SetJumpCount;
    [SerializeField]
    private int SetWallJumpCount;


   public CinemachineVirtualCamera playerCamera;
    public CinemachineFramingTransposer cameraTransposer;

    private void Awake()
    {
        if (Manager == null)
            Manager = this;

        if (Manager != null && Manager != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

   
    private void Start()
    {
        maxJumpCount = SetJumpCount;
        maxWallJumpCount = SetWallJumpCount;

        playerCamera = GetComponent<CinemachineVirtualCamera>();
       cameraTransposer = playerCamera.GetCinemachineComponent<CinemachineFramingTransposer>();       
    }

    private void Update()
    {
        stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        hp = Mathf.Clamp(hp, 0, maxHealth);
    }
    public void LookForThePlayer()
    {
        playerCamera.m_Follow = player.transform;
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}
