using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFunctionTrigger : MonoBehaviour
{
    
   public void CreateAudioSound(AudioClip clip)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = clip;
        audio.Play();
    }
}
