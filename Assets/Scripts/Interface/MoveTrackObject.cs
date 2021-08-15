using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MoveTrackObject : MonoBehaviour
{
    public CinemachineDollyCart cart;
    public float initialDelay;
    private float delay;
    public float Speed;
    public bool pingpong;
    private bool isDone;
    private bool isReached;
    public float Cooldown;
    private float cooldown;
    private float var;
    private bool isOn;



    void Start()
    {
        delay = initialDelay;
        isDone = false;
        isOn = false;
        isReached = false;
        cooldown = 0;
    }

    private void Update()
    {

        var = (!isReached) ? Speed : -Speed;

        if (cooldown >= 0)
            cooldown -= 1 * Time.deltaTime;
            if (isOn)
            {
                if (delay >= 0)
                    delay -= 1 * Time.deltaTime;
                else
                {
                    cart.m_Position += var * Time.deltaTime;
                }

            }

      
    }

    private void LateUpdate()
    {
        if (!isReached)
        {
            if (cart.m_Position == 1)
            {
                isOn = false;
                isReached = true;
                delay = initialDelay;
                cooldown = Cooldown;
            }

        }
        else
        {
            if (cart.m_Position == 0)
            {
                isOn = false;
                isReached = false;
                delay = initialDelay;
                cooldown = Cooldown;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (cooldown >= 0)
                return;

            if (!isDone)
            {
                if (!pingpong)
                    isDone = true;

                isOn = true;

            }
        }
    }
}
