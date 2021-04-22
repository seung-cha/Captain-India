using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStoneColor : MonoBehaviour
{
    public bool manipulateFirst;
    public bool manipulateSecond;
    public bool manipulateThird;
    public bool manipulateFourth;

    public ShatteredStone targetStone;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetStone.isComplete)
            return;

        if (collision.tag != "Player")
            return;


        if(manipulateFirst)
        {
           Switch(ref targetStone.firstStone);
        }

        if (manipulateSecond)
        {
            Switch (ref targetStone.secondStone);
        }

        if (manipulateThird)
        {
            Switch (ref targetStone.thirdStone);
        }

        if (manipulateFourth)
        {
            Switch (ref targetStone.fourthStone);
        }
    }


    private void Switch(ref bool target)
    {
        if (target)
            target = false;
        else
            target = true;
    }
}
