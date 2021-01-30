using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Manager;

    public bool isPaused;
    public GameObject pauseMenu;
    public List<int> gameScenes;
    void Start()
    {
        if (Manager == null)
            Manager = this;

        if (Manager != this && Manager != null)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameScenes.Contains(SceneManager.GetActiveScene().buildIndex))
            {
                // if the game is running, pause the game. Not, unpause the game
               if(!isPaused)
                {
                    Time.timeScale = 0.0f;
                    pauseMenu.SetActive(true);
                    isPaused = true;
                }
               else
                {
                    Time.timeScale = 1.0f;
                    pauseMenu.SetActive(false);
                    isPaused = false;
                }

            }
        }
    }
}
