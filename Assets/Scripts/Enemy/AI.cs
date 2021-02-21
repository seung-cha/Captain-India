using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    float scale;
    public float speed;
    public float moveOffset;
    public float distance;
    public float stopDistance;
    public float chaseHeightLimit;
    public int health;
   
   public Delay Dattack;
   public Delay Ddetection;

    public BoxCollider2D boxCol;
    public LayerMask playerMask;
    public Vector2 attackSize;
    public Vector2 attackBoxOffset;
    private Vector2[] wallPos;

    public bool showGizmo;

    [SerializeField]
    private bool targetIsOnLeft;

    public bool detectionForceFalse;
    public bool attackForceFalse;
    GameObject player;

    [SerializeField]
    float playerDistance;
    [SerializeField]
    private float minimumStopDistance;
    [SerializeField]
    private float heightDifference;
    public Rigidbody2D rid;
    Vector2 vel;

    public bool isStaggered;
    public float staggerDuration;
    public Vector2 staggerDir;


    public GameObject particleGameObject;
    public float duration;
    private float particleSpawnOffset;

    public bool isGrounded;
    public Vector2 boxSize;
    public Vector2 boxOffset;
    public LayerMask groundLayer;
    private Vector2 footPosition;
    private bool quitting = false;

    public float groundGravity;
    public float gravity;


    public bool unInterruptable;
    private void Awake()
    {
       
        scale = Mathf.Abs(transform.localScale.x);
        wallPos = new Vector2[2];
        player = PlayerManager.Manager.GetPlayer();
        // Left
     
    }

    private void OnApplicationQuit()
    {
        quitting = true;
    }
    private void OnDestroy()
    {
        if (!quitting)
        {
            if (health <= 0)
            {
                GameObject particle = Instantiate(particleGameObject);
                particle.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + particleSpawnOffset);
                Destroy(particle, duration);
            }
        }

        if (EnemyHolder.Manager.enemyAIs != null)
        {
            if (EnemyHolder.Manager.enemyAIs.ContainsKey(this.gameObject))
            {          
                EnemyHolder.Manager.enemyAIs.Remove(this.gameObject);
            }
        }  

    }
    void Start()
    {
        rid = GetComponent<Rigidbody2D>();

        if(EnemyHolder.Manager.enemyAIs != null)
        EnemyHolder.Manager.enemyAIs.Add(this.gameObject, this);
        particleSpawnOffset = boxCol.bounds.extents.y;
        unInterruptable = false;
    }

    // Update is called once per frame
    void Update()
    {
         playerDistance = Vector2.Distance(this.gameObject.transform.position, player.transform.position);
        minimumStopDistance = Mathf.Abs(this.gameObject.transform.position.x - player.transform.position.x);
        heightDifference = Mathf.Abs(this.gameObject.transform.position.y - player.transform.position.y);
        footPosition = new Vector2(boxCol.bounds.center.x, boxCol.bounds.min.y);


        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        Flip();
        AttackDetection();
        DetectPlayer();
        GroundDetection();
    }

    
    private void FixedUpdate()
    {
        Movement();
        rid.velocity = vel;
    }

    private void LateUpdate()
    {
        Dattack.CoolDown();
        /*
        if(unInterruptable)
        {
            isStaggered = false;
            staggerDuration = 0f;
            staggerDir = Vector2.zero;
        }
        */

    }


    void Flip()
    {
        if (PlayerManager.Manager.player != null)
        {
            if (this.gameObject.transform.position.x > PlayerManager.Manager.player.transform.position.x)
            {
                // Captain India is on the left side
                this.gameObject.transform.localScale = new Vector3(scale, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
                targetIsOnLeft = true;
            }
            else
            {
                this.gameObject.transform.localScale = new Vector3(-scale, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
                targetIsOnLeft = false;
            }
        }

    }

    void AttackDetection()
    {
        if(attackForceFalse || isStaggered)
        { Dattack.able = false; return; }

        wallPos[0] = new Vector2(boxCol.bounds.min.x, boxCol.bounds.center.y);
        // Right
        wallPos[1] = new Vector2(boxCol.bounds.max.x, boxCol.bounds.center.y);

        bool detectedPlayerLeft = Physics2D.OverlapBox(new Vector2(wallPos[0].x - attackBoxOffset.x, wallPos[0].y + attackBoxOffset.y), attackSize, 0f, playerMask);
        bool detectedPlayerRight = Physics2D.OverlapBox(new Vector2(wallPos[1].x + attackBoxOffset.x, wallPos[1].y + attackBoxOffset.y), attackSize, 0f, playerMask);

        if (Dattack.currentDelay <= 0f)
        {
            if (detectedPlayerLeft || detectedPlayerRight)
            {
                if (Dattack.threshHold <= 0)
                {

                    if (PlayerManager.Manager.player != null)
                    {
                        Debug.Log("Detected");
                        Dattack.able = true;
                        Dattack.currentDelay = Dattack.delay;
                        Dattack.threshHold = Dattack.threshHoldMax;
                    }
                }
                else
                    Dattack.threshHold = Dattack.threshHold - 1 * Time.deltaTime;
            }
        }
        else
            Dattack.able = false;

    }

    void DetectPlayer()
    {
      

        if (Ddetection.currentDelay <= 0f)
            Ddetection.currentDelay = 0f;

      
       

        // Debug.Log("Distance is " + Vector2.Distance(this.gameObject.transform.position, player.transform.position));
        if (!detectionForceFalse)
        {
           // RaycastHit2D[] hit = Physics2D.RaycastAll(boxCol.bounds.center, PlayerManager.Manager.transform.position, Mathf.Infinity, playerAndThisObject);
           // foreach(RaycastHit2D hits in hit)
           // {
           //     Debug.Log(hits.collider.tag);
           // ra }
           
            if (playerDistance <= distance && heightDifference <= chaseHeightLimit)
            {
              
                    Ddetection.able = true;
                    Ddetection.currentDelay = Ddetection.delay;
                
            }


            if (playerDistance >= distance)
            {
                Ddetection.currentDelay -= 1 * Time.deltaTime;
            }
        }

        if( Ddetection.currentDelay <= 0)
        {
            Ddetection.able = false;
        }

        if (PlayerManager.Manager.player == null)
            Ddetection.able = false;

    }

    void Movement()
    {
        if (!isStaggered)
        {
            if (!detectionForceFalse)
            {
                if (Ddetection.able == true)
                {
                    // Start chasing if the player is within range
                    if (playerDistance >= moveOffset)
                    {

                        if (targetIsOnLeft && minimumStopDistance >= stopDistance)
                        {
                            vel = new Vector2(-speed, vel.y);
                        }
                        else if (!targetIsOnLeft && minimumStopDistance >= stopDistance)
                        {
                            vel = new Vector2(speed, vel.y);
                        }
                        else
                            vel = new Vector2(0, vel.y);
                    }
                    else
                        vel = new Vector2(0, vel.y);
                }
                else
                    vel = new Vector2(0, vel.y);
            }
            else
            {
                vel = new Vector2(0, vel.y);
            }
        }
        else
        {
            vel = staggerDir;
            staggerDuration -= 1 * Time.deltaTime;

            if (staggerDuration <= 0)
            {
                isStaggered = false;
                detectionForceFalse = false;
            }
        }
    }

    private void GroundDetection()
    {
        isGrounded = Physics2D.BoxCast(footPosition +boxOffset, boxSize, 0f, Vector2.zero, 0f, groundLayer);

        if (isGrounded)
        {

            rid.gravityScale = groundGravity;
        }
        else
        rid.gravityScale = gravity;
    }


    private void OnDrawGizmos()
    {
        if (!showGizmo)
            return;

        Gizmos.color = Color.red;

        if (!attackForceFalse)
        {
            Gizmos.DrawCube(new Vector2(wallPos[0].x - attackBoxOffset.x, wallPos[0].y + attackBoxOffset.y), attackSize);
            Gizmos.DrawCube(new Vector2(wallPos[1].x + attackBoxOffset.x, wallPos[1].y + attackBoxOffset.y), attackSize);
        }

        Gizmos.DrawLine(boxCol.bounds.center, PlayerManager.Manager.player.transform.position);
        Gizmos.DrawCube(footPosition + boxOffset, boxSize);

    }

  public void DisableDetection()
    {
        detectionForceFalse = true;
    }    

    public void EnableDetection()
    {
        detectionForceFalse = false;
    }

    public void DisableAttack()
    {
        attackForceFalse = false;
    }
    public void EnableAttack()
    {
        attackForceFalse = true;
    }

    public void canStagger()
    {
        unInterruptable = false;
    }
    public void cannotStagger()
    {
        unInterruptable = true;
    }
}


[System.Serializable]
public class Delay
{
    public float currentDelay;
    public float threshHold;
    public float threshHoldMax;
    public float delay;
    public bool able;


   public void CoolDown()
    {

        if(currentDelay >= 0)
        {
            currentDelay = currentDelay - 1 * Time.deltaTime;
        }
    }
}
