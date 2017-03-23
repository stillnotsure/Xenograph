using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    private ScenePlayer scenePlayer;
    public enum States
    {
        instructions,
        preTrial,
        trial,
        postTrial
    }
    public States state;


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

        DontDestroyOnLoad( this.gameObject );

        scenePlayer = gameObject.GetComponent<ScenePlayer>();
    }

    public void CommenceTrial()
    {
        AudioManager am = AudioManager.GetInstance();
        am.PlayBGMusic();
        state = States.trial;
        scenePlayer.Begin();
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
}
