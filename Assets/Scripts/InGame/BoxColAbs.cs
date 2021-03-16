using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColAbs : MonoBehaviour
{
    public BoxCollider col;
    
    void LateUpdate()
    {
        col.size = new Vector3(col.size.x, col.size.y, Mathf.Abs(col.size.z));
    }
}
