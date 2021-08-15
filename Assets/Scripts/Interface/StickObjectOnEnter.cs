using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickObjectOnEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Enemy")
        {
            collision.transform.parent = this.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            collision.transform.parent = null;
        }
    }
}
