using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class _Button_ : MonoBehaviour
{
    public Toggle bt;
    void Start()
    {
        bt.isOn = GameConfig.Config.skipTutorial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
