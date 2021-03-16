using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystemForceField particleField;
    public ParticleSystem particleScript;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeGravity(float value)
    {
        particleField.gravity = value;
    }
}
