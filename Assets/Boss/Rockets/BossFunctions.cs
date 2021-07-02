using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFunctions : MonoBehaviour
{
    public GameObject homingRocket;
    public float speed;
    public GameObject spawnPoint;
    
    public void CreateHomingRocket(float rotationSpeed_)
    {
        GameObject obj = Instantiate(homingRocket);
        Rocket_Homing scrpt = obj.GetComponent<Rocket_Homing>();
        scrpt.rotationSpeed = speed;
        scrpt.speed = rotationSpeed_;
        scrpt.transform.position = new Vector3 (spawnPoint.transform.position.x, spawnPoint.transform.position.y,0);
    }
}
