using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Manager;
    [SerializeField]
    private List<LoadingScreenInfo> loadingInfos;

    public int sceneIndex;
    public LoadingScreenInfo currentInfo;
    private int index;


    private void Awake()
    {
        if (Manager == null)
            Manager = this;

        if (Manager != null && Manager != this)
            Destroy(Manager.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        Shuffle();
    }

   public void Shuffle()
    {
        index = Random.Range(0, loadingInfos.Count);
        currentInfo = loadingInfos.ElementAt(index);
    }
}
