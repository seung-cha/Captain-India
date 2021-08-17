using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GuideManager : MonoBehaviour
{
    public static GuideManager Manager;
    [SerializeField]

    public List<GameObject> guides;
    public GameObject guideBody;
    public GameObject guideAlert;
    private int currentIndex;
    private bool isOpened;
    private void Awake()
    {
        if (Manager == null)
            Manager = this;

        if (Manager != null && Manager != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        currentIndex = 0;
        isOpened = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(!PauseManager.Manager.notGameScenes.Contains(SceneManager.GetActiveScene().buildIndex))
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (isOpened)
                {
                    isOpened = false;

                }
                else
                {
                    isOpened = true;
                    DisplayGuide();
                }
            }

            ShowGuide();
            ProcessGuide();

        }
      else
        {
            guideBody.SetActive(false);
            guideAlert.SetActive(false);
        }


    }

    void ShowGuide()
    {
        if (PauseManager.Manager.notGameScenes.Contains(SceneManager.GetActiveScene().buildIndex))
            return;

            if (isOpened)
        {
            guideBody.SetActive(true);
            guideAlert.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            guideBody.SetActive(false);
            guideAlert.SetActive(true);
            currentIndex = 0;
            Time.timeScale = 1f;
        }
    }

    void ProcessGuide()
    {
        if(isOpened)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentIndex--;

                if (currentIndex == -1)
                    currentIndex = 0;

                DisplayGuide();

            }
            
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentIndex++;

                if (currentIndex == guides.Count)
                    currentIndex = guides.Count - 1;

                DisplayGuide();


            }

       

          
        }
    }

    void DisplayGuide()
    {
        if(isOpened)
        {
            for(int i = 0; i < guides.Count; i++)
            {
                if (i == currentIndex)
                    guides[i].SetActive(true);
                else
                    guides[i].SetActive(false);
            }
        }
    }
}
