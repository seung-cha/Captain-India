using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class HidePlayer : MonoBehaviour
{
    public GameObject videoPlayer;
    public GameObject videoRawImage;
    private VideoPlayer player;

    private void Start()
    {
        player = videoPlayer.GetComponent<VideoPlayer>();
    }


    public void HideVideoPlayer()
    {
        Invoke("Hide", (float)player.length);
    }
    
    public void CancelHide()
    {
        CancelInvoke();
    }

    private void Hide()
    {
        videoRawImage.SetActive(false);
    }
}
