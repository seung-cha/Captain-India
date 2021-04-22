using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class PlayTimeLine : MonoBehaviour
{
    public PlayableDirector director;
    private bool playedOnce;



    private void Start()
    {
        playedOnce = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && playedOnce == false)
        {
            director.Play();
            playedOnce = true;
        }
    }
}
