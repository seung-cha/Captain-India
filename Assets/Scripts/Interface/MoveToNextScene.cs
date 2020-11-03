using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextScene : MonoBehaviour
{
    public float delay;
    public int sceneNumber;
    public bool toLoadingScreen;
  public void GoToNextScene()
    {
        StartCoroutine(ChangeScene());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(delay);

        if (toLoadingScreen)
        {
            LoadingScreenManager.Manager.sceneIndex = sceneNumber;
            SceneManager.LoadScene(1);
        }
        else
            SceneManager.LoadScene(sceneNumber);
    }
}
