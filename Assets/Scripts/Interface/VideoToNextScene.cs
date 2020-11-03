using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoToNextScene : MonoBehaviour
{
    private VideoPlayer vidPlayer;
    private float currentVideoTime;
    private float maxVideoTime;
    [SerializeField]
    private int sceneNumber;
    [SerializeField]
    private float delay;
    [SerializeField]
    private bool toLoadingScreen;

    [SerializeField]
    private bool skipable;

    bool hasCalled;
    private void Awake()
    {
        vidPlayer = GetComponent<VideoPlayer>();
        hasCalled = false;
    }
    void Start()
    {
        maxVideoTime = vidPlayer.frameCount -2;
    }

    // Update is called once per frame
    void Update()
    {       
        currentVideoTime = vidPlayer.frame;
        if (currentVideoTime >= maxVideoTime)
        {
            if(!hasCalled)
            {
                hasCalled = true;
                StartCoroutine(ToNextScene());
            }
        }

        if(skipable)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                LoadScene();
        }



    }


    IEnumerator ToNextScene()
    {
        yield return new WaitForSeconds(delay);

        LoadScene();
    }


    void LoadScene()
    {
        if (toLoadingScreen)
        {
            LoadingScreenManager.Manager.sceneIndex = sceneNumber;
            SceneManager.LoadScene(1);
        }
        else
            SceneManager.LoadScene(sceneNumber);
    }
}
