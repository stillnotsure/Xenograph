using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    void Awake()
    {
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
        gameObject.GetComponent<AudioSource>().clip = gameObject.GetComponent<AudioAssets>().bgMusic;
        gameObject.GetComponent<AudioSource>().Play();
    }
}