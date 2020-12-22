using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public float stunDuration;
    public float knockbackMultiplier;
    public Collider2D hitbox;


    private void Start()
    {
        hitbox.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerManager.Manager.hp -= damage;
            PlayerManager.Manager.staggerDuration = stunDuration;

            // if The player is on the left side of the enemy           
            if (PlayerManager.Manager.transform.position.x < this.gameObject.transform.position.x)
            {
                Vector2 value = new Vector2(1, 1).normalized;
                Vector2 staggerDirection = new Vector2(value.x * -knockbackMultiplier, 1);
                PlayerManager.Manager.staggerDirection = staggerDirection;
            }
            else
            {              
                Vector2 value = new Vector2(1, 1).normalized;
                Vector2 staggerDirection = new Vector2(value.x * knockbackMultiplier, 1);
                PlayerManager.Manager.staggerDirection = staggerDirection;
            }

            PlayerManager.Manager.isStaggered = true;
        }
    }

    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }

    public void DisableHitbox()
    {
        hitbox.enabled = false;
    }
}
