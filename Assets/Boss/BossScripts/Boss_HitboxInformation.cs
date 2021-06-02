using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_HitboxInformation : MonoBehaviour
{
    public int damage;
    public Vector2 pushbackDirection;
    public float multiply;
    public float duration;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            Debug.Log("Player Got Hit");


            if(!(PlayerManager.Manager.hp <= 0) || PlayerManager.Manager.player != null)
            {
                if(!PlayerManager.Manager.isInvincible)
                {
                    PlayerManager.Manager.hp -= damage;

                    if(!PlayerManager.Manager.unInterruptable)
                    {
                        PlayerManager.Manager.staggerDirection = pushbackDirection.normalized * multiply;
                        PlayerManager.Manager.staggerDuration = duration;
                        PlayerManager.Manager.isStaggered = true;
                    }

                }
            }

        }
    }
}
