using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectForBoss : MonoBehaviour
{
   void CreateSoundEffect(AudioClip aud)
    {
        SoundManager.Manager.CrateSoundEffect(aud, this.gameObject.transform.position);
    }
}
