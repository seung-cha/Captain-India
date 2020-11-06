using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider2D attackCollider;

   public void EnableCollider()
    {
        attackCollider.gameObject.SetActive(true);
    }

    public void DisableCollider()
    {
        attackCollider.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform != this.gameObject.transform && collision.gameObject.tag == "Enemy")
            collision.gameObject.SetActive(false);
    }
}
