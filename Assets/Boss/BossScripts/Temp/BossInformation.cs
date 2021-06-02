using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossInformation : MonoBehaviour
{
     public int bossHP;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("SoemthingEntered");


        if (collision.transform.tag == "PlayerHitbox")
        {
            // Boss damage
            Debug.Log("hitboxDeteceted");
            bossHP--;
        }

        if(collision.transform.tag == "Player")
        {
            // Player damage
            Debug.Log("Hurtbox Deteceted");
            PlayerManager.Manager.hp -= 10;
        }
    }


  


}
