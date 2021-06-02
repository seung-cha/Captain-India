using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDamage : MonoBehaviour
{
    public int damage;
    public float staggerDuration;
    public float staggerIntensityX;
    public float staggerIntensityY;
    private Vector2 staggerDirection;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (PlayerManager.Manager.player == null || PlayerManager.Manager.isInvincible || PlayerManager.Manager.hp <= 0)
                return;

            if (!PlayerManager.Manager.unInterruptable)
            {

                if (PlayerManager.Manager.player.transform.position.x > this.gameObject.transform.position.x)
            { // on Rightside
                staggerDirection = new Vector2(1f * staggerIntensityX, 1f * staggerIntensityY);
                
            }
            else
            {
                staggerDirection = new Vector2(-1f * staggerIntensityX, 1f * staggerIntensityY);
            }

           
                PlayerManager.Manager.isStaggered = true;
                PlayerManager.Manager.staggerDuration = staggerDuration;
                PlayerManager.Manager.staggerDirection = staggerDirection;
            }
            PlayerManager.Manager.hp -= damage;
            Destroy(this.gameObject);
        }
        else if (collision.tag == "FieldObject")
        Destroy(this.gameObject);
    }
}
