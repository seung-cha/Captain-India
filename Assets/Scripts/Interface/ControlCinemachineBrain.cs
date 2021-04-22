using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ControlCinemachineBrain : MonoBehaviour
{
    CinemachineBrain brain;
    public bool DisableBrain;
    void Start()
    {
        brain = PlayerManager.Manager.gameObject.GetComponentInChildren<CinemachineBrain>();

        if (DisableBrain)
            brain.enabled = false;
        else
            brain.enabled = true;

    }

   
}
