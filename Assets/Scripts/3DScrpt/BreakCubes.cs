using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class BreakCubes : MonoBehaviour
{
    public ShatteredStone target;
    private bool playedOnce;
    public PlayableDirector play;
    void Start()
    {
        playedOnce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.isComplete)
        {
            if(!playedOnce)
            {
                playedOnce = true;
                play.Play();
            }
        }
    }
}
