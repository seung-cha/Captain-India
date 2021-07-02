using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnableCinemachine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Manager.gameObject.GetComponentInChildren<CinemachineBrain>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
