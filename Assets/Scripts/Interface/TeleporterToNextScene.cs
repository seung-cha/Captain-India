using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterToNextScene : MonoBehaviour
{
    public int sceneNumber;
  public  bool toLoadingScreen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (toLoadingScreen)
            {
                LoadingScreenManager.Manager.sceneIndex = sceneNumber;
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(sceneNumber);
            }
        }
    }
}
