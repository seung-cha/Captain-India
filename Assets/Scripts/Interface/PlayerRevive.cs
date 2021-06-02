using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRevive : MonoBehaviour
{
    
    void Start()
    {
        PlayerManager.Manager.revivePos = this.gameObject.transform.position;
    }

  
}
