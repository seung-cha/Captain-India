using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Manager;
    
    [SerializeField]
    private GameObject soundGameObject;

    [SerializeField]
    AudioMixerGroup bgmMixerGroup;
    [SerializeField]
    AudioMixerGroup effectMixerGroup;
    [SerializeField]
    AudioMixerGroup voiceMixerGroup;

    private void Awake()
    {     
            if (Manager == null)
                Manager = this;

            if (Manager != null && Manager != this)
                Destroy(this.gameObject);

            DontDestroyOnLoad(this.gameObject);
    }

    public void CrateSoundEffect(AudioClip audio, Vector2 spawnPosition)
    {
        if(audio == null)
        { return; }

        GameObject obj = Instantiate(soundGameObject);
        AudioSource aud = obj.GetComponent<AudioSource>();

        obj.transform.position = spawnPosition;
        aud.clip = audio;
        aud.outputAudioMixerGroup = effectMixerGroup;
        aud.Play();
        Destroy(obj, aud.clip.length + 0.5f);

    }
}
