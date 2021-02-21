using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastFireball : MonoBehaviour
{
   public GameObject fireball;
    public GameObject spawnPoint;
    public float speed;
    public void ShootFireball()
    {
        GameObject obj = Instantiate(fireball);
        obj.transform.position = spawnPoint.transform.position;
        Rigidbody2D rid = obj.GetComponent<Rigidbody2D>();

        if(PlayerManager.Manager.player.transform.position.x > this.gameObject.transform.position.x)
        { // on Rightside
            obj.transform.localScale = new Vector3(-obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);
            rid.velocity = new Vector2(speed, 0f);
        }
        else
        {
            rid.velocity = new Vector2(-speed, 0f);
        }
    }
}
