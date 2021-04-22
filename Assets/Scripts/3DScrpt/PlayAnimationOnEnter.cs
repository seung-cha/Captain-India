using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationOnEnter : MonoBehaviour
{
    public Animator anim;
    public string playString;
    public bool allowMultiplePlay;
    private bool played;

    private void Start()
    {
        played = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!played)
                anim.Play(playString);

            if (!allowMultiplePlay)
                played = true;
        }
    }
}
