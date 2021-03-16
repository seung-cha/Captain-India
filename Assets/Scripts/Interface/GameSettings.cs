using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GameSettings : MonoBehaviour
{
    Resolution[] res;
    public TMP_Dropdown resDropDown;

    int currentIndex;
    private void Start()
    {
        res = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToArray();
        resDropDown.ClearOptions();

        List<string> resOptions = new List<string>();

        for(int i = 0; i < res.Length; i++)
        {
            string resolution = res[i].width + " x " + res[i].height;
            resOptions.Add(resolution);

            if (res[i].width == Screen.currentResolution.width && res[i].height == Screen.currentResolution.height)
                currentIndex = i;
        }

        resDropDown.AddOptions(resOptions);
        resDropDown.value = currentIndex;
    }
    public void ToggleFullScreen(bool state)
    {
        Screen.fullScreen = state;
    }

    public void ChangeResolution (int index)
    {
        Screen.SetResolution(res[index].width, res[index].height, Screen.fullScreen);
    }
   
}
