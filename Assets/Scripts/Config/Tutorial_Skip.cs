using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Skip : MonoBehaviour
{
    public bool DisableGameObjectOnSkip;
    void Start()
    {
        if (!DisableGameObjectOnSkip)
        {
            if (GameConfig.Config.skipTutorial)
                gameObject.SetActive(true);
            else
                gameObject.SetActive(false);
        }
        else
        {
            if (GameConfig.Config.skipTutorial)
                gameObject.SetActive(false);
            else
                gameObject.SetActive(true);
        }


    }


}
