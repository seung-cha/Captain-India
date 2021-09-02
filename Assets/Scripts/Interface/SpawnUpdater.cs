using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnUpdater : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerManager.Manager.revivePos = this.transform.position;
        }
    }
}
