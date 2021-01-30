using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfiner : MonoBehaviour
{

   public Collider2D boundCollider;
    void Start()
    {
        PlayerManager.Manager.cinemachineConfiner.m_BoundingShape2D = boundCollider;
    }

    private void OnDestroy()
    {
        PlayerManager.Manager.cinemachineConfiner.m_BoundingShape2D = null;
    }

}
