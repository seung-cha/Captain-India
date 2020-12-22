using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    AI charAI;
    Animator anim;
    public string IdleString;
    public string moveString;
    public string hurtString;
    public string attackString;
    void Start()
    {
        anim = GetComponent<Animator>();

        charAI = GetComponent<AI>();
    }

 
    void Update()
    {
        // is Chasing
        if (!charAI.detectionForceFalse)
        {
            if (charAI.Ddetection.able)
            {

                if (charAI.rid.velocity != Vector2.zero)
                {
                    anim.Play(moveString);
                }
                else
                {
                    anim.Play(IdleString);
                }
            }
            else
                anim.Play(IdleString);

        }
        else
        {
           
        }
           

        if (charAI.Dattack.able)
        {
            anim.Play(attackString);
        }
    }
}
