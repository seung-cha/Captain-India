using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Config;

    public bool skipTutorial;

    private void Awake()
    {
        if (Config == null)
            Config = this;

        if (Config != null && Config != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
