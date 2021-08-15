using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustSpeed : MonoBehaviour
{
    public float playerEffectSpeed;
    public float rocketEffectSpeed;
    public float rocketMinSpeed;

    private Dictionary<GameObject, float> rockets;
    void Start()
    {
        rockets = new Dictionary<GameObject, float>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerManager.Manager.defaultSpeed += playerEffectSpeed;
        }

        if(collision.tag == "Rocket")
        {
            Rocket_Homing rocket = collision.GetComponent<Rocket_Homing>();
            rockets.Add(collision.gameObject, rocket.speed);

            if (rocket.speed + rocketEffectSpeed < rocketMinSpeed)
            {
                rocket.speed = rocketMinSpeed;
            }
            else
            {
                rocket.speed += rocketEffectSpeed;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerManager.Manager.defaultSpeed -= playerEffectSpeed;
        }

        if (collision.tag == "Rocket")
        {       
            
            if(rockets.ContainsKey(collision.gameObject))
            {
                Rocket_Homing rocket = collision.GetComponent<Rocket_Homing>();
                rocket.speed = rockets[collision.gameObject];
                rockets.Remove(collision.gameObject);
            }
        }
    }
    private void OnDestroy()
    {
        foreach(KeyValuePair<GameObject, float> pair in rockets)
        {
            Rocket_Homing rocket = pair.Key.gameObject.GetComponent<Rocket_Homing>();
            rocket.speed = pair.Value;
        }
    }
}
