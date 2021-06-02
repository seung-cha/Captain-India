using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionInGround : MonoBehaviour
{
    public Collider2D col;
    public float offset;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Manager.player == null || PlayerManager.Manager.hp <= 0)
        {
            return;
        }

        if (this.transform.position.y + offset >= PlayerManager.Manager.player.transform.position.y)
        {
            Physics2D.IgnoreCollision(PlayerManager.Manager.player.transform.GetComponent<Collider2D>(), col, true);
        }
        else
            Physics2D.IgnoreCollision(PlayerManager.Manager.player.transform.GetComponent<Collider2D>(), col, false);
    }
}
