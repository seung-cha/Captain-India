using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInformationContainer
{
   public  float health;
}
public class Example : MonoBehaviour
{

   int damage;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Determine if the game object interacted with the hitbox is the player.
        if(collision.transform.tag == "Player")
        {
            // Damage the player
            PlayerManager.Manager.hp -= damage;
        }
    }
}
