using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage;
    public float stunDuration;
    public float knockbackMultiplier;
    public Collider2D hitbox;

    public AudioClip temp;
    private void Start()
    {
        hitbox.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(PlayerManager.Manager.isInvincible || PlayerManager.Manager.hp <=0)
            { return; }

            PlayerManager.Manager.hp -= damage;
          

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

            if (!PlayerManager.Manager.unInterruptable)
            {
                PlayerManager.Manager.staggerDuration = stunDuration;
                PlayerManager.Manager.isStaggered = true;
            }
           // SoundManager.Manager.CrateSoundEffect(temp, collision.gameObject.transform.position);
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
