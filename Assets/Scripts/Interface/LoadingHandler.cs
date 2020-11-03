using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingHandler : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public RawImage backgroundImage;
    public Button loadingButton;

    AsyncOperation operation;
    private void Awake()
    {
        LoadingScreenManager.Manager.Shuffle();
    }
    void Start()
    {
        descriptionText.text = LoadingScreenManager.Manager.currentInfo.text;
        backgroundImage.texture = LoadingScreenManager.Manager.currentInfo.background;

        operation = SceneManager.LoadSceneAsync(LoadingScreenManager.Manager.sceneIndex, LoadSceneMode.Single);
        loadingButton.interactable = false;
        operation.allowSceneActivation = false;
    }

    private void Update()
    {
        if(!operation.isDone)
        {
           if(operation.progress >= 0.9f)
                loadingButton.interactable = true;
        }      
    }



    public void ChangeScene()
    {
        operation.allowSceneActivation = true;
    }
}
