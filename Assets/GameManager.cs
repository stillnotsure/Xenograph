using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    private AudioManager am;
    private ScenePlayer scenePlayer;
    private UIManager uiManager;
    private RecordChecker recordChecker;
    private float timer;

    //Timers 
    private float preTrialDuration = 5f;
    private float postTrialDuration = 5f;
    private float fadeOutDuration = 5f;
    public float score = 0;

    private Dictionary<States, float> stateTimers;

    public enum States
    {
        instructions,
        preTrial,
        trial,
        postTrial,
        fadeOut,
        recap
    }
    public States state;

    public void SetState(States state)
    {
        this.state = state;
        float tryTimer;
        if (stateTimers.TryGetValue(state, out tryTimer))
        {
            timer = tryTimer;
        }
    }

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

        stateTimers = new Dictionary<States, float>();
        stateTimers.Add(States.preTrial, preTrialDuration);
        stateTimers.Add(States.postTrial, postTrialDuration);
        stateTimers.Add(States.fadeOut, fadeOutDuration);
        SetState(States.instructions);
    }

    void Start()
    {
        am = AudioManager.GetInstance();
        uiManager = UIManager.GetInstance();
        recordChecker = gameObject.GetComponent<RecordChecker>();
    }
    void Update()
    {
        switch (state)
        {
            case (States.preTrial): PreTrial();
                break;
            case (States.postTrial): PostTrial();
                break;
            case (States.fadeOut): FadeOut();
                break;
            default: 
                break;
        }
    }

    public void CommenceTrial()
    {
        am.PlayBGMusic();
        scenePlayer.Begin();
    }

    public void PreTrial()
    {
        if (timer <= 0)
        {
            CommenceTrial();
            SetState(States.trial);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void PostTrial()
    {
        if (timer <= 0)
        {
            SetState(States.fadeOut);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    //Need to set timer to postTrialDuration before this point
    public void FadeOut()
    {
        uiManager.FadeScreenToBlack(postTrialDuration);
        if (timer <= 0)
        {
            score = recordChecker.CheckRecords();
            SceneManager.LoadScene("Recap");
            SetState(States.recap);
        } else
        {
            timer -= Time.deltaTime;
        }
    }

    public static GameManager GetInstance()
    {
        return instance;
    }
}
