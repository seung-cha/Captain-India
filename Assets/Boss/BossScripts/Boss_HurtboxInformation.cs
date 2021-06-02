using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_HurtboxInformation : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "PlayerHitbox")
        {
            Debug.Log("Boss Got Hit");

        }
    }
}
