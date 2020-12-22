using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    float scale;
    public float speed;
    public float moveOffset;
    public float distance;
   
   public Delay Dattack;
   public Delay Ddetection;

    public BoxCollider2D boxCol;
    public LayerMask playerMask;
    public LayerMask playerAndThisObject;
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

   public Rigidbody2D rid;
    Vector2 vel;

    
    private void Awake()
    {
        scale = Mathf.Abs(transform.localScale.x);
        wallPos = new Vector2[2];
        player = PlayerManager.Manager.GetPlayer();
        // Left
    }
    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(this.gameObject.transform.position, player.transform.position);
        Flip();
        AttackDetection();
        DetectPlayer();
       
    }

    private void FixedUpdate()
    {
        Movement();
        rid.velocity = vel;
    }

    private void LateUpdate()
    {
        Dattack.CoolDown();

    }


    void Flip()
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

    void AttackDetection()
    {
        if(attackForceFalse)
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
                    Debug.Log("Detected");
                    Dattack.able = true;
                    Dattack.currentDelay = Dattack.delay;
                    Dattack.threshHold = Dattack.threshHoldMax;
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
           // }
           
            if (playerDistance <= distance)
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

    }

    void Movement()
    {

        if (!detectionForceFalse)
        {
            if (Ddetection.able == true)
            {
                if (playerDistance >= moveOffset)
                {

                    if (targetIsOnLeft)
                    {
                        vel = new Vector2(-speed, vel.y);
                    }
                    else if (!targetIsOnLeft)
                    {
                        vel = new Vector2(speed, vel.y);
                    }
                }
                else
                    vel = Vector2.zero;
            }
            else
                vel = Vector2.zero;
        }
        else
        {
            vel = Vector2.zero;
        }
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
