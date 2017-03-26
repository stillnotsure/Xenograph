using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private AudioSource localAudioSource;

    void Awake()
    {
        localAudioSource = GetComponent<AudioSource>();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        //DontDestroyOnLoad( this.gameObject );
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    public void PlayBGMusic()
    {
        localAudioSource.clip = gameObject.GetComponent<AudioAssets>().assets[0];
        localAudioSource.Play();
    }

    public void PlayOnce(AudioSource audioSource, AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}