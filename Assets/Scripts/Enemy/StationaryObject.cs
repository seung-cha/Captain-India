using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryObject : MonoBehaviour
{
    public bool fixRotation;
    public bool fixPosition;

    private Vector2 originalPosition;
    private Vector2 originalRotation;
    private void Awake()
    {
        originalPosition = this.gameObject.transform.position;
        originalRotation = this.gameObject.transform.localScale;
    }
    private void LateUpdate()
    {
        if(fixRotation)
        {
            this.gameObject.transform.localScale = originalRotation;
        }

        if(fixPosition)
        {
            this.gameObject.transform.position = originalPosition;
                
        }
    }
}
