using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateHP : MonoBehaviour
{
    void Start()
    {
        PlayerManager.Manager.hp = 100;
        PlayerManager.Manager.stamina = 100;
        PlayerManager.Manager.reviveCount = 5;
    }


}
