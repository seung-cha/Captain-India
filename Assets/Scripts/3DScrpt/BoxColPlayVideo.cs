using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BoxColPlayVideo : MonoBehaviour
{
    public BoxCollider2D boxCol;
    public VideoPlayer vidPlayer;
    public float increaseValueBy;
    private float threshHold;
    private bool isOutside;

    private bool hasPlayedOnce;
    public bool rePlay;
    void Start()
    {
        threshHold = 0f;
        vidPlayer.Pause();
        isOutside = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOutside)
            threshHold -= increaseValueBy * Time.deltaTime;

        threshHold = Mathf.Clamp(threshHold, 0, 1);
        vidPlayer.playbackSpeed = threshHold;

        if (vidPlayer.length <= vidPlayer.time)
        {
            vidPlayer.Pause();
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isOutside = false;

            if (!vidPlayer.isPlaying &&!hasPlayedOnce)
                vidPlayer.Play();

            if (!rePlay)
                hasPlayedOnce = true;

            threshHold += increaseValueBy * Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isOutside = true;
    }
}
