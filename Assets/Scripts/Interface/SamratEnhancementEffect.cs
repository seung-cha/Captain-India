using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.LWRP;

public class SamratEnhancementEffect : MonoBehaviour
{
    public Volume volume;

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Manager.enhanced)
            volume.weight = 1f;
        else
            volume.weight = 0f;
    }
}
