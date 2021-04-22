using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeConFig : MonoBehaviour
{
  public void ToggleTutorialSkip()
    {
        if (GameConfig.Config.skipTutorial)
            GameConfig.Config.skipTutorial = false;
        else
            GameConfig.Config.skipTutorial = true;
    }
}
