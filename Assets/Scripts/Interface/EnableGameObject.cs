using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObject : MonoBehaviour
{
    public bool hideThisGameObjectOnStart;


    void Start()
    {
        if (hideThisGameObjectOnStart)
        {
            this.gameObject.SetActive(false);
            Debug.Log("HideCAlled");
        }
    }

   public void OnThisGameObjectClick(GameObject obj)
    {
        obj.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
